define([
    "dojo/_base/connect",
    "dojo/_base/declare",
    "dojo/dom-class",
    "dojo/dom-style",
    "dojo/dom-attr",
    "dojo/dom-construct",
    "dojo/dom-prop",
    "dojo/_base/lang",

    "dijit/focus",
    "dijit/form/Button",
    "dijit/form/TextBox",
    "dijit/_Widget",
        "dijit/_TemplatedMixin",
        "dijit/_WidgetsInTemplateMixin",

    "epi/epi",
    "epi/shell/widget/_ValueRequiredMixin",
    "epi-cms/widget/_HasChildDialogMixin",
    "epi-cms/legacy/LegacyDialogPopup",

    "dojo/text!./Templates/BitForm.html",
    "dojo/domReady!"
],
function (
    connect,
    declare,
    domClass,
    domStyle,
    domAttr,
    domConstruct,
    domProp,
    lang,

    focusManager,
    button,
    textbox,
    _Widget,
    _TemplatedMixin,
    _WidgetsInTemplateMixin,

    epi,
    _ValueRequiredMixin,
    _HasChildDialogMixin,
    LegacyDialogPopup,

    template
) {
    return declare("bit.editors.BitForm", [
        _Widget,
        _TemplatedMixin,
        _WidgetsInTemplateMixin,
        _ValueRequiredMixin
    ], {
        templateString: template,
        _dialog: null,
        isShowingChildDialog: false,
        intermediateChanges: false,
        postCreate: function () {
            console.clear();

            this.updateWidget();
            this.inherited(arguments);
        },
        updateWidget: function () {
            domAttr.set(this.output, "disabled", "disabled");

            var val = this.value;

            if (val == null) {
                domStyle.set(this.openData, "display", "none");
                this.output.set("value", "Click the button to edit");
            } else {
                domStyle.set(this.openData, "display", "inherit");
                var id = val.formId;
                id = id.split(':')[0];
                this.output.set("value", "[" + id + "] " + val.formName);
            }
        },
        onChange: function (newvalue) {
            // Event that tells EPiServer when the widget's value has changed.        
        },
        openDialog: function (evt) {
            var that = this;

            this._dialog = new LegacyDialogPopup({
                url: this.formLivePath,
                dialogArguments: this.value,
                showCancelButton: true,
                closeIconVisible: false,
                features: { width: 640, height: 330 },
                autoFit: false,
                showLoadingOverlay: false,
                destroyOnHide: true,
                confirmActionText: "Cancel"
            });

            window.addEventListener('message', function (e) {
                if (e.data.type == 'BitFormValue'){
                    that._dialog.dialogArguments = e.data.data;
                }
            }, false);


            var actionBar = dojo.query(".dijit.dijitToolbar", this._dialog.actionContainerNode)[0];

            new dijit.form.Button({
                "label": "Select",
                onClick: function() {
                    that._DialogPanelAction('select');
                }
            }).placeAt(actionBar);
            new dijit.form.Button({
                "label": "No Form",
                onClick: function() {
                    that._DialogPanelAction('clear');
                }
            }).placeAt(actionBar);
            new dijit.form.Button({
                "label": "Cancel",
                onClick: function() {
                    that._DialogPanelAction('cancel');
                }
            }).placeAt(actionBar);

            this.connect(this._dialog, 'onHide', function () {
                this.isShowingChildDialog = false;
            });
            this.connect(this._dialog, "onShow", function () {
                this.isShowingChildDialog = true;
            });

            this._dialog.show();
        },
        _setValue: function (value) {
            this.onFocus();

            this.value = value;
            this._set("value", value);
            this.onChange(value);

            this.updateWidget();

            this.onBlur();
        },
        _DialogPanelAction: function (action) {
            switch (action) {
                case "select":
                    this._setValue(this._dialog.dialogArguments);
                    break;
                case "clear":
                    this._setValue(null);
                    break;
            }

            this._dialog.hide();
        }
    });
});