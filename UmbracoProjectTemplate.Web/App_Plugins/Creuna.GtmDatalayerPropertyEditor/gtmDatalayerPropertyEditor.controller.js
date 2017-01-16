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
        _.each(_.keys($scope.model.value), function (key) {
                if (!_.contains($scope.attrs, key)) {
                    delete $scope.model.value[key];
                }
            });
        
        console.info($scope.model.value);
    }
})();