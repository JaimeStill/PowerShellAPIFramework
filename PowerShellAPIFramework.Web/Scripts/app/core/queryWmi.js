(function () {
    var queryWmi = function (wmiSvc, toastrSvc) {
        return {
            restrict: 'EA',
            replace: true,
            templateUrl: '/Content/templates/query-wmi.html',
            link: function (scope) {
                scope.model = wmiSvc.queryModel;
                scope.pagedModel = {
                    results: []
                };
                scope.resultsModel = wmiSvc.resultsModel;
                scope.loading = false;

                scope.addProperty = function () {
                    if (scope.newProperty !== null && scope.newProperty !== undefined && scope.newProperty !== '') {
                        if (scope.model.properties.indexOf(scope.newProperty) === -1) {
                            scope.model.properties.push(scope.newProperty);
                            scope.newProperty = '';
                        } else {
                            toastrSvc.alertWarning('The specified property has already been added', 'Invalid Property Value');
                        }
                    } else {
                        toastrSvc.alertWarning('Property must have a value', 'Invalid Property Value');
                    }
                }

                scope.removeProperty = function (property) {
                    var index = scope.model.properties.indexOf(property);

                    if (index > -1) {
                        scope.model.properties.splice(index, 1);
                    } else {
                        toastrSvc.alertWarning('The specified property was not found in the property collection', 'Invalid Property Value');
                    }
                }

                scope.testQuery = function () {
                    if (validate(scope.model)) {
                        scope.loading = true;
                        scope.resultsModel.results = [];
                        wmiSvc.queryWmi(scope.model).finally(function () {
                            scope.loading = false;
                        });
                    }
                }

                function validate(model) {
                    if (model.query === null || model.query === undefined || model.query === '') {
                        toastrSvc.alertWarning('Query must have a value', 'Invalid Query Configuration');
                        return false;
                    }

                    if (model.properties === null || model.properties === undefined || model.properties.length < 1) {
                        toastrSvc.alertWarning('You must specify at least one property', 'Invalid Query Configuration');
                        return false;
                    }

                    if (model.computername === null || model.computername === undefined || model.computername === '') {
                        toastrSvc.alertWarning('You must specify the name of the computer to query', 'Invalid Query Configuration');
                        return false;
                    }

                    if (model.wmiNamespace === null || model.wmiNamespace === undefined || model.wmiNamespace === '') {
                        toastrSvc.alertWarning('You must specify the WMI Namespace', 'Invalid Query Configuration');
                        return false;
                    }

                    return true;
                }
            }
        }
    };

    queryWmi.$inject = ['wmiSvc', 'toastrSvc'];
    powershellApp.directive('queryWmi', queryWmi);
}());