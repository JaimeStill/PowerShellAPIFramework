(function () {
    var pagedContainer = function () {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/paged-container.html',
            replace: true,
            transclude: true,
            scope: {
                itemCollection: '=',
                pagedCollection: '='
            },
            link: function (scope) {
                scope.pageSizes = [5, 10, 20];
                scope.pageSize = 20;

                scope.$watch(function () { return scope.itemCollection.results.length; },
                    function (newValue, oldValue) {
                        if (newValue !== null && newValue !== undefined && newValue !== 0) {
                            scope.updatePaging();
                        } else {
                            scope.pagedCollection.results = [];
                        }
                    });

                scope.previous = function () {
                    if (scope.currentPage > 0) {
                        scope.currentPage--;
                        scope.pagedCollection.results = scope.itemCollection.results.slice(scope.currentPage * scope.pageSize, (scope.currentPage * scope.pageSize) + scope.pageSize);
                    }
                }

                scope.next = function () {
                    if (scope.currentPage < (scope.numberOfPages - 1)) {
                        scope.currentPage++;
                        scope.pagedCollection.results = scope.itemCollection.results.slice(scope.currentPage * scope.pageSize, (scope.currentPage * scope.pageSize) + scope.pageSize);
                    }
                }

                scope.updatePaging = function () {
                    scope.currentPage = 0;
                    scope.numberOfPages = Math.ceil(scope.itemCollection.results.length / scope.pageSize);
                    scope.pagedCollection.results = scope.itemCollection.results.slice(scope.currentPage * scope.pageSize, (scope.currentPage * scope.pageSize) + scope.pageSize);
                }
            }
        }
    };

    powershellApp.directive('pagedContainer', pagedContainer);
}());