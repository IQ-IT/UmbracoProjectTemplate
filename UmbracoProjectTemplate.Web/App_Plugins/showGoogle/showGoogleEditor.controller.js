(function() {
	'use strict';

	angular.module('umbraco').controller('umb.showGoogleEditorController', editorController);
	editorController.$inject = ['$scope'];

		function editorController($scope) {
			$scope.limitTo = 100;
		}
})();