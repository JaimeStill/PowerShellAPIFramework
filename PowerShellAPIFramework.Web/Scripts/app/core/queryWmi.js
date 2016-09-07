(function () {
    var queryWmi = function (wmiSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/query-wmi.html',
            link: function (scope) {
                scope.queryModel = wmiSvc.queryModel;

                scope.addProperty = function () {
                    if (scope.newProperty !== null && scope.newProperty !== undefined && scope.newProperty !== '') {
                        if (scope.queryModel.properties.indexOf(scope.newProperty) === -1) {
                            scope.queryModel.properties.push(scope.newProperty);
                            scope.newProperty = '';
                        }
                    }
                }

                scope.removeProperty = function (property) {
                    var index = scope.queryModel.properties.indexOf(property);

                    if (index > -1) {
                        scope.queryModel.properties.splice(index, 1);
                    }
                }

                scope.testQuery = function () {
                    if (validate(scope.queryModel)) {
                        wmiSvc.queryWmi(scope.queryModel);
                    }
                }

                scope.$evalAsync(function () { return scope.queryModel.results; });

                function validate(model) {
                    if (model.query === null || model.query === undefined || model.query === '') {
                        return false;
                    }

                    if (model.properties === null || model.properties === undefined || model.properties.length < 1) {
                        return false;
                    }

                    if (model.computername === null || model.computername === undefined || model.computername === '') {
                        return false;
                    }

                    if (model.wmiNamespace === null || model.wmiNamespace === undefined || model.wmiNamespace === '') {
                        return false;
                    }

                    return true;
                }
            }
        };
    };

    powerShellApp.directive('queryWmi', queryWmi);
}());