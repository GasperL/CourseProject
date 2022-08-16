"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
exports.__esModule = true;
exports.CategoryComponent = void 0;
var core_1 = require("@angular/core");
var CategoryComponent = /** @class */ (function () {
    function CategoryComponent(service) {
        this.service = service;
        this.categories = [];
        this.title = "category";
    }
    CategoryComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.service.getCategories().subscribe(function (dto) {
            _this.categories = dto;
        });
    };
    CategoryComponent = __decorate([
        core_1.Component({
            selector: 'category-component',
            templateUrl: './category.component.html',
            styleUrls: ['./category.component.sass']
        })
    ], CategoryComponent);
    return CategoryComponent;
}());
exports.CategoryComponent = CategoryComponent;
