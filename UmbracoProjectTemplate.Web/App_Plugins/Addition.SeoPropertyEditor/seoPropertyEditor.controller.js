/* global angular */

(function() {
    'use strict';

    angular.module('umbraco').controller('Addition.SeoPropertyController', seoPropertyController);
    seoPropertyController.$inject = ['$scope', 'assetsService', 'dialogService'];

    function seoPropertyController($scope, assetsService, dialogService) {
        assetsService.loadCss('/App_Plugins/Addition.SeoPropertyEditor/seoPropertyEditor.css');
        $scope.model.value = $scope.model.value || {
            title: '',
            description: '',
            noIndex: false,
            noFollow: false,
            canonicalId: null,
            canonicalName: ''
        }
        $scope.limitchars_title = function() {
            var limit = $scope.model.config.limit_title;
            if ($scope.model.value.title.length > limit) {
                $scope.info_title = 'You cannot write more then ' + limit + ' characters!';
            } else {
                $scope.info_title = 'You have ' + (limit - $scope.model.value.title.length) + ' characters left.';
            }
        };
        $scope.limitchars_description = function() {
            var limit = $scope.model.config.limit_description;
            if ($scope.model.value.description.length > limit) {
                $scope.info_description = 'You cannot write more then ' + limit + ' characters!';
            } else {
                $scope.info_description = 'You have ' + (limit - $scope.model.value.description.length) + ' characters left.';
            }
        };
        $scope.pickLink = function() {
            dialogService.contentPicker({
                multipicker: false,
                callback: function(response) {
                    $scope.model.value.canonicalId = response.id;
                    $scope.model.value.canonicalName = response.name;
                }
            });
        };
        $scope.removePickLink = function() {
            $scope.model.value.canonicalId = null;
            $scope.model.value.canonicalName = '';
        };
    }
})();