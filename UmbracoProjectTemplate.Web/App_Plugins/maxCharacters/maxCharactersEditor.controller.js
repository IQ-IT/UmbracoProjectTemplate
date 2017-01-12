(function() {
	'use strict';

	angular.module('umbraco').controller('umb.maxCharactersEditorController', editorController);
	editorController.$inject = ['$scope'];

		function editorController($scope) {

			$scope.limitchars = function () {
	      var limit = $scope.model.config.limit;
				if ($scope.model.value.length > limit ) {
					$scope.info = 'You cannot write more then ' + limit  + ' characters!';
				}
				else {
					$scope.info = 'You have ' + (limit - $scope.model.value.length) + ' characters left.';
				}
	    };
		}
})();