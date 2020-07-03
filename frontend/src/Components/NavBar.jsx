import React, { Component } from "react";
import AuthProvider from '../Stores/AuthProvider';
import { view } from '@risingstack/react-easy-state';
import { AppBar, Tabs, Tab } from '@material-ui/core';
import { Switch, Route, Redirect, Link } from "react-router-dom";
import Badge from '@material-ui/core/Badge';
import { withRouter } from "react-router-dom";
class NavBar extends Component {
    handleChange = (event, value) => {
        this.props.history.push(value);
    };
    render() {
        const classes = this.props.classes;
        let route = '/' + this.props.history.location.pathname.split('/')[1];
        return (
            <AppBar position="static" color="default" title="Sharp Counter">
                <Tabs
                    value={route}
                    onChange={this.handleChange}
                    indicatorColor="primary"
                    textColor="primary"
                >
                    <Tab label="About" value="/about" />
                    <Tab label="Websites" value="/websites" />
                    {AuthProvider.isLoggedIn ?
                        <Tab label="Logout" onClick={AuthProvider.ToggleIsLoggedIn} /> :
                        <Tab label="Login" onClick={AuthProvider.ToggleIsLoggedIn} />}
                </Tabs>
            </AppBar >
        )
    }
}
export default withRouter(view(NavBar))
