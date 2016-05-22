(function() {
    'use strict';

    angular
        .module('app')
        .directive('movie', movie_directive);

    movie_directive.$inject = ['$window'];
    
    function movie_directive ($window) {
        var directive = {
            link: link,
            templateUrl: 'app/movie/movieDirective.html',
            restrict: 'EA',
            scope: {
                data: '='
            }
        };
        return directive;

        function link(scope, element, attrs) {
        }
    }

})();