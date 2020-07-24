module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["wwwroot/lib/*", "temp/"],
        //concat: {
        //    all: {
        //        src: ['wwwroot/js/table.js', 'wwwroot/js/site.js'],
        //        dest: 'wwwroot/js/combined.js'
        //    }
        //},
        cssmin: {
            target: {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/css',
                    src: ['*.css', '!*.min.css'],
                    dest: 'wwwroot/css',
                    ext: '.min.css'
                }]
            }
        },
        uglify: {
            options: {
                mangle: {
                    properties: true
                },
                compress: {
                    dead_code: true
                },
            },
            site: {
                src: ['wwwroot/js/site.js'],
                dest: 'wwwroot/js/prod/site.min.js'
            },
            table: {
                src: ['wwwroot/js/table.js'],
                dest: 'wwwroot/js/prod/table.min.js'
            },
            detailsChart: {
                src: ['wwwroot/js/detailsChart.js'],
                dest: 'wwwroot/js/prod/detailsChart.min.js'
            },
            count: {
                src: ['wwwroot/js/count.js'],
                dest: 'wwwroot/js/prod/count.min.js'
            }
        },
        watch: {
            files: ["wwwroot/js/*.js"],
            tasks: ["all"]
        }
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.registerTask("all", ['clean', 'concat', 'uglify']);
}
