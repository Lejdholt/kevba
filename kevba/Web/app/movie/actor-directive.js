(function() {
    'use strict';

    angular
        .module('app')
        .directive('actor', actor);

    actor.$inject = ['$window'];
    
    function actor($window) {
        var directive = {
            link: link,
            templateUrl: 'app/movie/actorDirective.html',
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