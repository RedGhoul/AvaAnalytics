(function () {
    'use strict';

    if (window.sharpcounter && window.sharpcounter.vars)
        window.sharpcounter = window.sharpcounter.vars
    else
        window.sharpcounter = window.sharpcounter || {}

    var get_data = function (vars) {
        var data = {
            p: (vars.path === undefined ? sharpcounter.path : vars.path),
            r: (vars.referrer === undefined ? sharpcounter.referrer : vars.referrer),
            t: (vars.title === undefined ? sharpcounter.title : vars.title),
            e: !!(vars.event || sharpcounter.event),
            s: [window.screen.width, window.screen.height, (window.devicePixelRatio || 1)],
            b: is_bot(),
            q: location.search,
        }
        if (!localStorage.currSession) {
            localStorage.setItem('currSession', Math.random().toString(36).substr(2, 5));
        }

        var rcb, pcb, tcb
        if (typeof (data.r) === 'function') rcb = data.r
        if (typeof (data.t) === 'function') tcb = data.t
        if (typeof (data.p) === 'function') pcb = data.p
        if (is_empty(data.r)) data.r = document.referrer
        if (is_empty(data.t)) data.t = document.title
        if (is_empty(data.p)) {
            var loc = location,
                c = document.querySelector('link[rel="canonical"][href]')
            if (c) {
                var a = document.createElement('a')
                a.href = c.href
                if (a.hostname.replace(/^www\./, '') === location.hostname.replace(/^www\./, ''))
                    loc = a
            }
            data.p = (loc.pathname + loc.search) || '/'
        }

        if (rcb) data.r = rcb(data.r)
        if (tcb) data.t = tcb(data.t)
        if (pcb) data.p = pcb(data.p)
        return data
    }

    var is_empty = function (v) { return v === null || v === undefined || typeof (v) === 'function' }

    var is_bot = function () {
        var w = window, d = document
        if (w.callPhantom || w._phantom || w.phantom)
            return 150
        if (w.__nightmare)
            return 151
        if (d.__selenium_unwrapped || d.__webdriver_evaluate || d.__driver_evaluate)
            return 152
        if (navigator.webdriver)
            return 153
        return 0
    }

    var urlencode = function (obj) {
        var p = []
        for (var k in obj)
            if (obj[k] !== '' && obj[k] !== null && obj[k] !== undefined && obj[k] !== false)
                p.push(encodeURIComponent(k) + '=' + encodeURIComponent(obj[k]))
        return '?' + p.join('&')
    }

    var get_endpoint = function () {
        var s = document.querySelector('script[data-sharpcounter]');
        if (s && s.dataset.sharpcounter)
            return s.dataset.sharpcounter
        return (sharpcounter.endpoint || window.counter)
    }

    sharpcounter.filter = function () {
        if ('visibilityState' in document && (document.visibilityState === 'prerender' || document.visibilityState === 'hidden'))
            return 'visibilityState'
        if (!sharpcounter.allow_frame && location !== parent.location)
            return 'frame'
        //if (!sharpcounter.allow_local && location.hostname.match(/(localhost$|^127\.|^10\.|^172\.(1[6-9]|2[0-9]|3[0-1])\.|^192\.168\.)/))
        //    return 'local'
        if (localStorage && localStorage.getItem('skipgc') === 't')
            return 'disabled with #toggle-sharpcounter'
        return false
    }

    window.sharpcounter.url = function (vars) {
        var data = get_data(vars || {})
        if (data.p === null)  // null from user callback.
            return
        data.rnd = Math.random().toString(36).substr(2, 5)

        var endpoint = get_endpoint()
        if (!endpoint) {
            if (console && 'warn' in console)
                console.warn('sharpcounter: no endpoint found')
            return
        }

        return endpoint + urlencode(data)
    }

    // Count a hit.
    window.sharpcounter.count = function (vars) {
        var f = sharpcounter.filter()
        if (f) {
            if (console && 'log' in console)
                console.warn('sharpcounter: not counting because of: ' + f)
            return
        }

        var url = sharpcounter.url(vars)
        if (!url) {
            if (console && 'log' in console)
                console.warn('sharpcounter: not counting because path callback returned null')
            return
        }
        //sf
        var apiKey = document.querySelector('script[data-sharpcounter-apikey]');
        apiKey = apiKey.getAttribute("data-sharpcounter-apikey");
        var img = document.createElement('img')
        img.src = url + "&key=" + apiKey + "&session=" + localStorage.currSession;
        img.style.position = 'absolute'
        img.setAttribute('alt', '')
        img.setAttribute('aria-hidden', 'true')
        var rm = function () { if (img && img.parentNode) img.parentNode.removeChild(img) }
        setTimeout(rm, 3000)  // In case the onload isn't triggered.
        img.addEventListener('load', rm, false)
        document.body.appendChild(img)
    }

    // Get a query parameter.
    window.sharpcounter.get_query = function (name) {
        var s = location.search.substr(1).split('&')
        for (var i = 0; i < s.length; i++)
            if (s[i].toLowerCase().indexOf(name.toLowerCase() + '=') === 0)
                return s[i].substr(name.length + 1)
    }

    // Track click events.
    window.sharpcounter.bind_events = function () {
        if (!document.querySelectorAll)  // Just in case someone uses an ancient browser.
            return

        var send = function (elem) {
            return function () {
                sharpcounter.count({
                    event: true,
                    path: (elem.dataset.sharpcounterClick || elem.name || elem.id || ''),
                    title: (elem.dataset.sharpcounterTitle || elem.title || (elem.innerHTML || '').substr(0, 200) || ''),
                    referrer: (elem.dataset.sharpcounterReferrer || elem.dataset.sharpcounterReferral || ''),
                })
            }
        }

        Array.prototype.slice.call(document.querySelectorAll("*[data-sharpcounter-click]")).forEach(function (elem) {
            if (elem.dataset.sharpcounterBound)
                return
            var f = send(elem)
            elem.addEventListener('click', f, false)
            elem.addEventListener('auxclick', f, false)  // Middle click.
            elem.dataset.sharpcounterBound = 'true'
        })
    }

    // Make it easy to skip your own views.
    if (location.hash === '#toggle-sharpcounter')
        if (localStorage.getItem('skipgc') === 't') {
            localStorage.removeItem('skipgc', 't')
            alert('sharpcounter tracking is now ENABLED in this browser.')
        }
        else {
            localStorage.setItem('skipgc', 't')
            alert('sharpcounter tracking is now DISABLED in this browser until ' + location + ' is loaded again.')
        }

    if (!sharpcounter.no_onload) {
        var go = function () {
            sharpcounter.count()
            if (!sharpcounter.no_events)
                sharpcounter.bind_events()
        }

        if (document.body === null)
            document.addEventListener('DOMContentLoaded', function () { go() }, false)
        else
            go()
    }
})();