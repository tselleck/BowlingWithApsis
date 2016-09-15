(function () {
    'use strict';

    angular.module('app')
        .config(routes);

    // Setting up all routes in the project
    function routes($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'App/Views/main.html',
                controller: 'MainController',
                controllerAs: 'vm'
            });
    }
})();