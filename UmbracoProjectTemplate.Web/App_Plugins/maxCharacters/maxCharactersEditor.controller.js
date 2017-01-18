/* global angular */

(function () {
    'use strict';

    angular.module('umbraco').controller('umb.maxCharactersEditorController', editorController);
    editorController.$inject = ['$scope', 'dialogService', 'assetsService'];

    function editorController($scope, dialogService, assetsService) {
        assetsService.loadCss("/App_Plugins/maxCharacters/maxCharacters-editor.css");
        $scope.model.value = $scope.model.value ||
        {
            title: '',
            description: '',
            noIndex: false,
            noFollow: false,
            canonicalId: null
        };
        $scope.limitchars_title = function () {
            var limit = $scope.model.config.limit_title;
            if ($scope.model.value.title.length > limit) {
                $scope.info_title = 'You cannot write more then ' + limit + ' characters!';
            }
            else {
                $scope.info_title = 'You have ' + (limit - $scope.model.value.title.length) + ' characters left.';
            }
        };
        $scope.limitchars_description = function () {
            var limit = $scope.model.config.limit_description;
            if ($scope.model.value.description.length > limit) {
                $scope.info_description = 'You cannot write more then ' + limit + ' characters!';
            }
            else {
                $scope.info_description = 'You have ' + (limit - $scope.model.value.description.length) + ' characters left.';
            }
        };
        $scope.pickLink = function () {
            dialogService.contentPicker({
                multipicker: false,
                callback: function(response) {
                    $scope.model.value.canonicalId = response.id;
                }
            });
        };
    }
})();