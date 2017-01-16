/* global angular, _ */

(function() {
    'use strict';

    angular.module('umbraco').controller('Creuna.GtmDatalayerPropertyController', editorController);
    editorController.$inject = ['$scope'];

    function editorController($scope) {
        $scope.attrs = $scope.model.config.attributes.split(',');
        _.each($scope.attrs, function (attr) {
                if (!$scope.model.value.hasOwnProperty(attr))
                    $scope.model.value[attr] = '';
            });
        _.each($scope.model.value, function(p) {
                if (!$scope.model.value.hasOwnProperty(p)) return;
                if (_.contains($scope.attrs, p)) {
                    delete $scope.model.value[p];
                }
            });
        
        console.info($scope.model.value);
    }
})();