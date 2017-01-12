(function() {
	'use strict';

	angular.module('umbraco').controller('umb.maxCharactersEditorController', editorController);
	editorController.$inject = ['$scope'];

		function editorController($scope) {
			$scope.limitTo = 100;
		}
})();