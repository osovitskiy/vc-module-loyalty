angular.module('virtoCommerce.loyaltyModule')
.controller('virtoCommerce.loyaltyModule.loyaltyStatusListController', ['$scope', 'virtoCommerce.loyaltyModule.statuses', 'platformWebApp.dialogService', 'platformWebApp.bladeUtils', 'platformWebApp.uiGridHelper', function ($scope, statuses, dialogService, bladeUtils, uiGridHelper) {
    $scope.uiGridConstants = uiGridHelper.uiGridConstants;

    var blade = $scope.blade;
    var bladeNavigationService = bladeUtils.bladeNavigationService;

    blade.refresh = function () {
        blade.isLoading = true;

        statuses.all(function (data) {
            blade.currentEntities = data.statuses;
            blade.isLoading = false;

            //$scope.objects = data.statuses;
        }, function (error) { bladeNavigationService.setError('Error ' + error.status, blade); });
    }

    blade.delete = function() {
        
    }

    blade.showDetailBlade = function(data) {
        $scope.selectedNodeId = data.id;

        var newBlade = {
            id: 'storeDetails',
            currentEntity: data,
            controller: 'virtoCommerce.loyaltyModule.loyaltyStatusDetailController',
            template: 'Modules/$(VirtoCommerce.Loyalty)/Scripts/blades/loyalty-status-detail.tpl.html'
        };
        bladeNavigationService.showBlade(newBlade, blade);
    }

    function showCreateBlade() {
        $scope.selectedNodeId = null;

        var newBlade = {
            id: 'storeDetails',
            isNew: true,
            currentEntity: {},
            controller: 'virtoCommerce.loyaltyModule.loyaltyStatusDetailController',
            template: 'Modules/$(VirtoCommerce.Loyalty)/Scripts/blades/loyalty-status-detail.tpl.html'
        };
        bladeNavigationService.showBlade(newBlade, blade);
    }

    blade.toolbarCommands = [
        {
            name: "platform.commands.refresh", icon: 'fa fa-refresh',
            executeMethod: blade.refresh,
            canExecuteMethod: function () {
                return true;
            }
        },
        {
            name: "platform.commands.add", icon: 'fa fa-plus',
            executeMethod: showCreateBlade,
            canExecuteMethod: function () {
                return true;
            },
            permission: 'loyalty:create'
        }
    ];

    $scope.setGridOptions = function (gridOptions) {
        uiGridHelper.initialize($scope, gridOptions, function (gridApi) {
            uiGridHelper.bindRefreshOnSortChanged($scope);
        });
    };

    blade.refresh();
}]);
