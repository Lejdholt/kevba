(function () {

    angular.module('app')
        .config(function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise("/");

            $stateProvider
                .state('home', {
                    url: "/",
                    views: {
                        'main': {
                            templateUrl: "partials/movie.html",
                            controller: "movieController",
                            controllerAs: 'vm'
                        }

                    }
                })
        });
})();