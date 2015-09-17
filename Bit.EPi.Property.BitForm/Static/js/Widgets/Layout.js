require([
    "dojo/_base/declare",
    "dojo/dom",
    "dojo/dom-construct",
    "dojo/json",

    "dijit/_WidgetBase",
    "dijit/_TemplatedMixin",
    "dijit/_WidgetsInTemplateMixin",

    "dojo/text!" + dojoConfig.tplPath + "/Widgets/Layout.html",
    "dojo/text!/BitForm/GetDojoTree",

    "dojo/store/Memory",
    "dijit/tree/ObjectStoreModel",
    "dijit/Tree",

    "dijit/layout/BorderContainer",
    "dijit/layout/ContentPane",
    "dijit/layout/LayoutContainer",
    "dijit/layout/AccordionContainer",
    "dijit/layout/AccordionPane",
    "dijit/Tree",

    "dijit/form/Button",

    "bitform/Widgets/Buttons"
], function (declare,
             dom,
             construct,
             json,
             _WidgetBase,
             _TemplatedMixin,
             _WidgetsInTemplateMixin,
             template,
             data,
             Memory,
             ObjectStoreModel,
             Tree) {
    var a = {
            formId: "27:d155dcb9-d029-4e8a-9c89-69bda4cfc291",
            formName: "Test"
        },
        b = {
            formId: "28:d1717c37-0033-45da-a3aa-bb7ea46992fb",
            formName: "Test 2"
        };

    window.parent.postMessage({
        type: "BitFormValue",
        data: b
    }, window.location.origin);

    var Layout = declare("bitform/Widgets/Layout", [
        _WidgetBase,
        _TemplatedMixin,
        _WidgetsInTemplateMixin
    ], {
        templateString: template
    });

    var j = json.parse(data);

    var myStore = new Memory({
        data: j.data,
        getChildren: function(object){
            return this.query({parent: object.id});
        }
    });

    var myModel = new ObjectStoreModel({
        store: myStore,
        query: {name: 'Root'}
    });

    var tree = new Tree({
        model: myModel,
        style: {height: '100%'},
        getIconClass: function(/*dojo.store.Item*/ item, /*Boolean*/ opened){
            switch (item.type) {
                case 'root':
                case 'folder':
                    return opened ? "dijitFolderOpened" : "dijitFolderClosed";
                    break;
                default:
                    return "dijitLeaf";
            }
        }
    });

    dojo.addOnLoad(function () {
        tree.placeAt(dijit.byId("treePane"));
    });
});