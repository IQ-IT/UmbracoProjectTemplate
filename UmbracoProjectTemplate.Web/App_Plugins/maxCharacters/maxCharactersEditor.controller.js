/* global angular */

(function () {
    'use strict';

    angular.module('umbraco').controller('umb.maxCharactersEditorController', editorController);
    editorController.$inject = ['$scope'];

    function editorController($scope, dialogService) {

        $scope.limitchars_title = function () {
            var limit = $scope.model.config.limit_title;
            if ($scope.model.title.value.length > limit) {
                $scope.info_title = 'You cannot write more then ' + limit + ' characters!';
            }
            else {
                $scope.info_title = 'You have ' + (limit - $scope.model.title.value.length) + ' characters left.';
            }
        };
        $scope.limitchars_description = function () {
            var limit = $scope.model.config.limit_description;
            if ($scope.model.description.value.length > limit) {
                $scope.info_description = 'You cannot write more then ' + limit + ' characters!';
            }
            else {
                $scope.info_description = 'You have ' + (limit - $scope.model.description.value.length) + ' characters left.';
            }
        };
        $scope.pickLink = function () {
            console.log('pick link');
            dialogService.contentPicker({
                multipicker: false,
                callback: function(response) {
                    console.log(response);
                }
            });
        };
    }
})();