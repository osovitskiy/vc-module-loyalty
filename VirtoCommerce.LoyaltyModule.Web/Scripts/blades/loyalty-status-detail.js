angular.module('virtoCommerce.loyaltyModule')
.controller('virtoCommerce.loyaltyModule.loyaltyStatusDetailController', ['$scope', 'virtoCommerce.loyaltyModule.statuses', 'platformWebApp.dialogService', 'platformWebApp.bladeNavigationService',
    function ($scope, statuses, dialogService, bladeNavigationService) {
        var blade = $scope.blade;

        blade.updatePermission = 'loyalty:update';
        blade.subtitle = 'loyalty.blades.loyalty-status-detail.subtitle';

        blade.refresh = function (parentRefresh) {
            if (blade.isNew) {
                blade.isLoading = false;
            } else {
                blade.isLoading = true;

                statuses.get({ id: blade.currentEntity.id }, function (data) {
                    initializeBlade(data);

                    if (parentRefresh) {
                        blade.parentBlade.refresh();
                    }
                });
            }
        }

        function initializeBlade(data) {
            blade.currentEntity = angular.copy(data);
            blade.origEntity = data;

            if (!blade.isNew) {
                blade.title = data.name;
            }

            blade.isLoading = false;
        }

        function isDirty() {
            return !angular.equals(blade.currentEntity, blade.origEntity) && !blade.isNew && blade.hasUpdatePermission();
        }

        function canSave() {
            return isDirty() && $scope.formScope && $scope.formScope.$valid;
        }

        $scope.saveChanges = function () {
            blade.isLoading = true;

            if (blade.isNew) {
                statuses.save(blade.currentEntity, function() {
                    blade.parentBlade.refresh(true);
                    blade.origEntity = blade.currentEntity;
                    $scope.bladeClose();
                });
            } else {
                var entityToSave = angular.copy(blade.currentEntity);

                statuses.update({}, entityToSave, function (data) {
                    blade.refresh(true);
                });
            }
        };

        function deleteEntry() {
            var dialog = {
                id: "confirmDelete",
                title: "loyalty.dialogs.loyalty-status-delete.title",
                message: "loyalty.dialogs.loyalty-status-delete.message",
                callback: function (remove) {
                    if (remove) {
                        blade.isLoading = true;

                        statuses.remove({ ids: blade.currentEntity.id }, function () {
                            $scope.bladeClose();
                            blade.parentBlade.refresh();
                        }, function (error) {
                            bladeNavigationService.setError('Error ' + error.status, blade);
                        });
                    }
                }
            }
            dialogService.showConfirmationDialog(dialog);
        }

        $scope.setForm = function (form) { $scope.formScope = form; };

        blade.onClose = function (closeCallback) {
            bladeNavigationService.showConfirmationIfNeeded(isDirty(), canSave(), blade, $scope.saveChanges, closeCallback, "loyalty.dialogs.loyalty-status-save.title", "loyalty.dialogs.loyalty-status-save.message");
        };

        if (!blade.isNew) {
            blade.toolbarCommands = [
            {
                name: "platform.commands.save",
                icon: 'fa fa-save',
                executeMethod: $scope.saveChanges,
                canExecuteMethod: canSave,
                permission: blade.updatePermission
            },
            {
                name: "platform.commands.reset",
                icon: 'fa fa-undo',
                executeMethod: function () {
                    angular.copy(blade.origEntity, blade.currentEntity);
                },
                canExecuteMethod: isDirty,
                permission: blade.updatePermission
            },
            {
                name: "platform.commands.delete",
                icon: 'fa fa-trash-o',
                executeMethod: deleteEntry,
                canExecuteMethod: function () { return true; },
                permission: 'loyality:delete'
            }];
        }

        blade.refresh(false);
}]);