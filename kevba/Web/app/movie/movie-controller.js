(function () {
    'use strict';

    angular
        .module('app')
        .controller('movieController', movieController);

    movieController.$inject = ['$http'];
    function movieController($http) {
        var vm = this;
        vm.isFetching = false;
        vm.firstActor = '';
        vm.secondActor = '';
        vm.results = [];

        vm.canFetch = function () {
            if(vm.isFetching) {
                return false;
            }

            if(vm.firstActor === '' || vm.secondActor === '') {
                return false;
            }

            return true;
        }


        vm.goFetch = function () {
            if(!vm.canFetch()) {
                return;
            }
            vm.results = [];
            vm.isFetching = true;

            $http.get('/api/actor/connect/' + vm.firstActor + '/' + vm.secondActor)
                .success(function (data) {
                    angular.forEach(data, function (item) {
                        vm.results.push(item);
                    })
                })
                .error(function (status) {
                    console.log(status);
                })
                .finally(function () {
                    vm.isFetching = false;
                })
        }
    }
})();
