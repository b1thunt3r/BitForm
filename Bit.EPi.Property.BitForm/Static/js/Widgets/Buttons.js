require([
    "dojo/_base/declare",

    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

    "dojo/text!"+dojoConfig.tplPath+"/Widgets/Buttons.html",

    "dijit/form/Button"
], function(
    declare,

    _WidgetBase,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

    template
) {
    var Buttons = declare("bitform/Widgets/Buttons", [
        _WidgetBase,
        _TemplatedMixin,
        _WidgetsInTemplateMixin
    ], {
        imgPath: this.dojoConfig.imgPath,
        templateString: template
    });
});