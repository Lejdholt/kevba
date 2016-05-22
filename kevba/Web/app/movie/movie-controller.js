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
        vm.results = null;

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

            vm.results = null;
            vm.isFetching = true;

            $http.get('/api/actor/connect/' + vm.firstActor + '/' + vm.secondActor)
                .success(function (data) {
                    vm.results = data;
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
