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
                scope.propertiesModel = wmiSvc.propertiesModel;
                scope.loading = false;

                scope.toggle = function () {
                    scope.model.isRemoteConnection = !scope.model.isRemoteConnection;
                }

                scope.testQuery = function () {
                    if (validate(scope.model)) {
                        scope.loading = true;
                        scope.resultsModel.results = [];
                        scope.propertiesModel.properties = [];
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

                    if (model.computername === null || model.computername === undefined || model.computername === '') {
                        toastrSvc.alertWarning('You must specify the name of the computer to query', 'Invalid Query Configuration');
                        return false;
                    }

                    if (model.wmiNamespace === null || model.wmiNamespace === undefined || model.wmiNamespace === '') {
                        toastrSvc.alertWarning('You must specify the WMI Namespace', 'Invalid Query Configuration');
                        return false;
                    }

                    if (model.isRemoteConnection) {
                        if (model.username === null || model.username === undefined || model.username === '') {
                            toastrSvc.alertWarning('Username is required for remote connections', 'Invalid Query Configuration');
                            return false;
                        }

                        if (model.password === null || model.password === undefined || model.password === '') {
                            toastrSvc.alertWarning('Password is required for remote connection', 'Invalid QueryConfiguration');
                            return false;
                        }
                    }

                    return true;
                }
            }
        }
    };

    queryWmi.$inject = ['wmiSvc', 'toastrSvc'];
    powershellApp.directive('queryWmi', queryWmi);
}());