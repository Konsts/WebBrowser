(function() {
    'use strict';

    angular
        .module('app')
        .directive('directoriesList', function () {
            return {
                restrict: "E",
                templateUrl: "/app/directives/directoriesTemplate.html"
            }
        });
    

})();