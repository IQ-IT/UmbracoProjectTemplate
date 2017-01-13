/* global angular */

(function() {
    'use strict';

    angular.module('umbraco').controller('Creuna.SeoPropertyController', seoPropertyController);
    seoPropertyController.$inject = ['$scope'];

    function seoPropertyController($scope) {
        $scope.model.value = $scope.model.value ||
        {
            title: '',
            description: '',
            noIndex: false,
            noFollow: false,
            canonicalId: null
        }
    }
})();