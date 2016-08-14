(function () {
    'use strict';

    angular
        .module('app')
        .controller('browserController', ['$scope', 'directory',
        function ($scope, directory) {
            $scope.loading = true;

            $scope.dir = directory.get();

            $scope.getDir = function (dirPath) {
                $scope.loading = true;
                if (dirPath=="") {
                    $scope.dir = directory.get();
                    return;
                }
                directory.get({ action: 'GetDirectories', path: dirPath }, function (data) {
                    $scope.dir = data;
                });
                directory.get({ action: 'GetStatistic', path: dirPath }, function (data) {
                    $scope.stat = data;
                    $scope.loading = false;
                });
            }
        }]);
})();
