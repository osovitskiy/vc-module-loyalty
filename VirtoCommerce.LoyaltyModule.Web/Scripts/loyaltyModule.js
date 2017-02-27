//Call this to register our module to main application
var moduleTemplateName = "virtoCommerce.loyaltyModule";

if (AppDependencies != undefined) {
    AppDependencies.push(moduleTemplateName);
}

angular.module(moduleTemplateName, [])
.config(['$stateProvider', '$urlRouterProvider',
    function ($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('workspace.loyaltyModule', {
                url: '/loyalty',
                templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                controller: [
                    '$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
                        var newBlade = {
                            id: 'loyaltyStatusList',
                            title: 'loyalty.blades.loyalty-status-list.title',
                            controller: 'virtoCommerce.loyaltyModule.loyaltyStatusListController',
                            template: 'Modules/$(VirtoCommerce.Loyalty)/Scripts/blades/loyalty-status-list.tpl.html',
                            isClosingDisabled: true
                        };
                        bladeNavigationService.showBlade(newBlade);
                    }
                ]
            });
    }
])
.run(['$rootScope', 'platformWebApp.mainMenuService', 'platformWebApp.widgetService', '$state',
    function ($rootScope, mainMenuService, widgetService, $state) {
        //Register module in main menu
        var menuItem = {
            path: 'browse/loyalty',
            icon: 'fa fa-cube',
            title: 'loyalty.main-menu-title',
            priority: 100,
            action: function () { $state.go('workspace.loyaltyModule') },
            permission: 'loyalty:access'
        };
        mainMenuService.addMenuItem(menuItem);
    }
]);
