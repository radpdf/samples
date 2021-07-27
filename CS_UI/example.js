var api = null;
var rpHeight = 0;

// Run the value of the menu item on select
// Adapted from http://www.telerik.com/forums/accessing-the-original-menu-item-object-during-select-event
function kendoOnSelectRunValue(e)
{
    var item = $(e.item),
        menuElement = item.closest(".k-menu"),
        dataItem = this.options.dataSource,
        index = item.parentsUntil(menuElement, ".k-item").map(function () {
            return $(this).index();
        }).get().reverse();

    index.push(item.index());

    for (var i = -1, len = index.length; ++i < len;)
    {
        dataItem = dataItem[index[i]];
        dataItem = i < len-1 ? dataItem.items : dataItem;
    }

    if (dataItem.value)
    {
        dataItem.value();
    }
}

function radpdfOnResize()
{
    var ele = $("#radpdf");

    if (rpHeight)
    {
        ele.outerHeight(rpHeight);
    }
    else
    {
        ele.outerHeight($(window).innerHeight() - ele.offset().top);
    }

     $("#object-properties").outerHeight($(window).innerHeight());

    $("#object-properties table").css({
        "position" : "absolute",
        "left" : "50%",
        "top" : "50%",
        "margin-left" : function() {return -$(this).outerWidth()/2},
        "margin-top" : function() {return -$(this).outerHeight()/2}
    });

    api.refresh();
}

var opBorderColor,
    opBorderColorData,
    opBorderWidth,
    opBorderWidthData,
    opColor,
    opColorData,
    opFillColor,
    opFillColorData,
    opFontName,
    opFontNameData,
    opFontSize,
    opFontSizeData,
    opFontBold,
    opFontBoldData,
    opFontItalic,
    opFontItalicData,
    opFontUnderline,
    opFontUnderlineData,
    opLineWidth,
    opLineWidthData,
    opPositionLeft,
    opPositionLeftData,
    opPositionTop,
    opPositionTopData,
    opPositionWidth,
    opPositionWidthData,
    opPositionHeight,
    opPositionHeightData;

function setupProperties()
{
    // If already setup
    if (opBorderColor)
    {
        return;
    }

    opBorderColor = $("#op-border-color");
    opBorderWidth = $("#op-border-width");
    opColor = $("#op-color");
    opFillColor = $("#op-fill-color");
    opFontName = $("#op-font-name");
    opFontSize = $("#op-font-size");
    opFontBold = $("#op-font-bold");
    opFontItalic = $("#op-font-italic");
    opFontUnderline = $("#op-font-underline");
    opLineWidth = $("#op-line-width");
    opPositionLeft = $("#op-pos-left");
    opPositionTop = $("#op-pos-top");
    opPositionWidth = $("#op-pos-width");
    opPositionHeight = $("#op-pos-height");

    opBorderColorData = opBorderColor.find("input").kendoColorPicker(
    {
        buttons: false,
        clearButton: true,
        preview: true
    }).data("kendoColorPicker");
    opBorderWidthData = opBorderWidth.find("input").kendoNumericTextBox(
    {
        decimals: 0,
        format: "n0",
        max: 100,
        min: 0,
        restrictDecimals: true
    }).data("kendoNumericTextBox");
    opColorData = opColor.find("input").kendoColorPicker(
    {
        buttons: false,
        preview: true
    }).data("kendoColorPicker");
    opFillColorData = opFillColor.find("input").kendoColorPicker(
    {
        buttons: false,
        clearButton: true,
        preview: true
    }).data("kendoColorPicker");
    opFontNameData = opFontName.find("select").kendoDropDownList().data("kendoDropDownList");
    opFontSizeData = opFontSize.find("input").kendoNumericTextBox(
    {
        decimals: 0,
        format: "n0",
        max: 100,
        min: 0,
        restrictDecimals: true
    }).data("kendoNumericTextBox");
    opFontBoldData = opFontBold.find("input").kendoMobileSwitch().data("kendoMobileSwitch");
    opFontItalicData = opFontItalic.find("input").kendoMobileSwitch().data("kendoMobileSwitch");
    opFontUnderlineData = opFontUnderline.find("input").kendoMobileSwitch().data("kendoMobileSwitch");
    opLineWidthData = opLineWidth.find("input").kendoNumericTextBox(
    {
        decimals: 0,
        format: "n0",
        max: 100,
        min: 0,
        restrictDecimals: true
    }).data("kendoNumericTextBox");
    opPositionLeftData = opPositionLeft.find("input").kendoNumericTextBox(
    {
        decimals: 0,
        format: "n0",
        restrictDecimals: true
    }).data("kendoNumericTextBox");
    opPositionTopData = opPositionTop.find("input").kendoNumericTextBox(
    {
        decimals: 0,
        format: "n0",
        restrictDecimals: true
    }).data("kendoNumericTextBox");
    opPositionWidthData = opPositionWidth.find("input").kendoNumericTextBox(
    {
        decimals: 0,
        format: "n0",
        restrictDecimals: true
    }).data("kendoNumericTextBox");

    opPositionHeightData = opPositionHeight.find("input").kendoNumericTextBox(
    {
        decimals: 0,
        format: "n0",
        restrictDecimals: true
    }).data("kendoNumericTextBox");

    $("#op-save input").kendoButton();
}

function showProperties(o)
{
    // NOTE: LinkAnnotation and PopupAnnotation not yet handled.
    // NOTE: This function presumes object is unlocked.

    if (o)
    {
        setupProperties();

        opBorderColor.hide();
        opBorderWidth.hide();
        opColor.hide();
        opFillColor.hide();
        opFontName.hide();
        opFontSize.hide();
        opFontBold.hide();
        opFontItalic.hide();
        opFontUnderline.hide();
        opLineWidth.hide();

        var p = o.getProperties();
        var t = o.getType();

        switch (t)
        {
            case api.ObjectType.EllipseShape:
            case api.ObjectType.RectangleShape:
            case api.ObjectType.SignatureShape:
            case api.ObjectType.ButtonField:
            case api.ObjectType.CheckField:
            case api.ObjectType.ComboField:
            case api.ObjectType.ListField:
            case api.ObjectType.RadioField:
            case api.ObjectType.TextField:
            case api.ObjectType.CircleAnnotation:
            case api.ObjectType.SquareAnnotation:

                opBorderColorData.value(p.border.color || "none");
                opBorderColor.show();
                opBorderWidthData.value(p.border.width);
                opBorderWidth.show();
                break;
        }

        switch (t)
        {
            case api.ObjectType.CaretAnnotation:
            case api.ObjectType.CheckShape:
            case api.ObjectType.StrikeoutAnnotation:
            case api.ObjectType.UnderlineAnnotation:

                opColorData.value(p.color);
                opColor.show();
                break;

            case api.ObjectType.ArrowShape:
            case api.ObjectType.InkShape:
            case api.ObjectType.LineShape:

                opColorData.value(p.lineColor);
                opColor.show();
                opLineWidthData.value(p.lineWidth);
                opLineWidth.show();
                break;
        }

        switch (t)
        {
            case api.ObjectType.EllipseShape:
            case api.ObjectType.RectangleShape:
            case api.ObjectType.SignatureShape:
            case api.ObjectType.ButtonField:
            case api.ObjectType.CheckField:
            case api.ObjectType.ComboField:
            case api.ObjectType.ListField:
            case api.ObjectType.RadioField:
            case api.ObjectType.TextField:
            case api.ObjectType.CircleAnnotation:
            case api.ObjectType.SquareAnnotation:
            
                opFillColorData.value(p.fillColor || "none");
                opFillColor.show();
                break;
        }

        switch (t)
        {
            case api.ObjectType.TextShape:
            case api.ObjectType.ButtonField:
            case api.ObjectType.ComboField:
            case api.ObjectType.ListField:
            case api.ObjectType.TextField:

                opColorData.value(p.font.color);
                opColor.show();
                opFontNameData.value(p.font.name);
                opFontName.show();
                opFontSizeData.value(p.font.size);
                opFontSize.show();
                opFontBoldData.value(p.font.bold);
                opFontBold.show();
                opFontItalicData.value(p.font.italic);
                opFontItalic.show();
                if (api.ObjectType.TextShape == t)
                {
                    opFontUnderlineData.value(p.font.underline);
                    opFontUnderline.show();
                }
                break;
        }

        opPositionLeftData.value(p.left);
        opPositionTopData.value(p.top);
        opPositionWidthData.value(p.width);
        opPositionHeightData.value(p.height); 

        $("#op-save input").click(function()
        {
            var p =
            {
                left : opPositionLeftData.value(),
                top : opPositionTopData.value(),
                width : opPositionWidthData.value(),
                height : opPositionHeightData.value()
            };

            switch (t)
            {
                case api.ObjectType.EllipseShape:
                case api.ObjectType.RectangleShape:
                case api.ObjectType.SignatureShape:
                case api.ObjectType.ButtonField:
                case api.ObjectType.CheckField:
                case api.ObjectType.ComboField:
                case api.ObjectType.ListField:
                case api.ObjectType.RadioField:
                case api.ObjectType.TextField:
                case api.ObjectType.CircleAnnotation:
                case api.ObjectType.SquareAnnotation:

                    p.border = 
                    {
                        color: ("none" == opBorderColorData.value()) ? "" : opBorderColorData.value(),
                        width: opBorderWidthData.value()
                    };
                    break;
            }

            switch (t)
            {
                case api.ObjectType.CaretAnnotation:
                case api.ObjectType.CheckShape:
                case api.ObjectType.StrikeoutAnnotation:
                case api.ObjectType.UnderlineAnnotation:

                    p.color = opColorData.value();
                    break;

                case api.ObjectType.ArrowShape:
                case api.ObjectType.InkShape:
                case api.ObjectType.LineShape:

                    p.lineColor = opColorData.value();
                    p.lineWidth = opLineWidthData.value();
                    break;
            }

            switch (t)
            {
                case api.ObjectType.EllipseShape:
                case api.ObjectType.RectangleShape:
                case api.ObjectType.SignatureShape:
                case api.ObjectType.ButtonField:
                case api.ObjectType.CheckField:
                case api.ObjectType.ComboField:
                case api.ObjectType.ListField:
                case api.ObjectType.RadioField:
                case api.ObjectType.TextField:
                case api.ObjectType.CircleAnnotation:
                case api.ObjectType.SquareAnnotation:
            
                    p.fillColor = ("none" == opFillColorData.value()) ? "" : opFillColorData.value()
                    break;
            }

            switch (t)
            {
                case api.ObjectType.TextShape:
                case api.ObjectType.ButtonField:
                case api.ObjectType.ComboField:
                case api.ObjectType.ListField:
                case api.ObjectType.TextField:

                    p.font = 
                    {
                        color: opColorData.value(),
                        name: opFontNameData.value(),
                        size: opFontSizeData.value(),
                        bold: opFontBoldData.value(),
                        italic: opFontItalicData.value()
                    };

                    if (api.ObjectType.TextShape == t)
                    {
                        p.font.underline = opFontUnderlineData.value();
                    }
                    break;
            }

            o.setProperties(p);

            showProperties();
            return false;
        });

        $("#object-properties").fadeIn(200);
        radpdfOnResize();
    }
    else
    {
        $("#object-properties").fadeOut(200);
    }
}

function initRadPdf(a, height)
{
    // Cache api instance
    api = a;

    // Cache height
    rpHeight = height;

    // Add event handler for object added
    api.addEventListener("objectAdded", function(evt) 
        {
            if (evt.obj.getType() == api.ObjectType.ButtonField)
            {
                switch (addedButtonType)
                {
                    case "reset":
                        evt.obj.setProperties(
                            {
                                isReset: true,
                                label: "Reset"
                            });
                        break;

                    case "submit":
                        evt.obj.setProperties(
                            {
                                isSubmit: true,
                                label: "Submit"
                            });
                        break;
                }
            }
        });

    // Add menu
    var menu = $("#kendo-menu").kendoMenu({
        dataSource: [
            {
                cssClass: "MenuEdit",
                text: "Edit",
                enabled: false,
                items: [
                    {
                        cssClass: "MenuEditDelete",
                        text: "Delete",
                        value: function()
                        {
                            var o = api.getObjectSelected();

                            if(o)
                            {
                                o.deleteObject();
                            }
                        }
                    },
                    {
                        cssClass: "MenuEditProperties",
                        text: "Properties",
                        value: function()
                        {
                            var o = api.getObjectSelected();

                            if (o)
                            {
                                showProperties(o);
                            }
                        }
                    },
                    {
                        cssClass: "MenuEditUnlock",
                        text: "Unlock",
                        value: function()
                        {
                            var o = api.getObjectSelected();

                            if (o)
                            {
                                o.unlock();
                                o.select();
                            }
                        }
                    }
                ]
            },
            {
                text: "Insert",
                items: [
                    {
                        text: "Text",
                        value: function(){api.setMode(api.Mode.InsertTextShape);}
                    },
                    {
                        text: "Whiteout",
                        value: function(){api.setMode(api.Mode.InsertWhiteoutShape);}
                    },
                    {
                        text: "Image",
                        value: function(){api.setMode(api.Mode.InsertImageShape);}
                    },
                    {
                        text: "Freehand",
                        value: function(){api.setMode(api.Mode.InsertInkShape);}
                    },
                    {
                        text: "Link",
                        value: function(){api.setMode(api.Mode.InsertLinkAnnotation);}
                    },
                    {
                        text: "Form Field",
                        items: [
                            {
                                text: "Text",
                                value: function(){api.setMode(api.Mode.InsertTextField);}
                            },
                            {
                                text: "Checkbox",
                                value: function(){api.setMode(api.Mode.InsertCheckField);}
                            },
                            {
                                text: "Radio",
                                value: function(){api.setMode(api.Mode.InsertRadioField);}
                            },
                            {
                                text: "Dropdown",
                                value: function(){api.setMode(api.Mode.InsertComboField);}
                            },
                            {
                                text: "Listbox",
                                value: function(){api.setMode(api.Mode.InsertListField);}
                            },
                            {
                                text: "Reset Button",
                                value: function(){api.setMode(api.Mode.InsertButtonField);addedButtonType="reset";}
                            },
                            {
                                text: "Submit Button",
                                value: function(){api.setMode(api.Mode.InsertButtonField);addedButtonType="submit";}
                            }
                        ]
                    },
                    {
                        text: "Line",
                        value: function(){api.setMode(api.Mode.InsertLineShape);}
                    },
                    {
                        text: "Arrow",
                        value: function(){api.setMode(api.Mode.InsertArrowShape);}
                    },
                    {
                        text: "Rectangle",
                        value: function(){api.setMode(api.Mode.InsertRectangleShape);}
                    },
                    {
                        text: "Circle",
                        value: function(){api.setMode(api.Mode.InsertEllipseShape);}
                    },
                    {
                        text: "Checkmark",
                        value: function(){api.setMode(api.Mode.InsertCheckShape);}
                    }
                ]
            },
            {
                text: "Annotate",
                items: [
                    {
                        text: "Sticky Note",
                        value: function(){api.setMode(api.Mode.InsertTextAnnotation);}
                    },
                    {
                        text: "Highlight",
                        value: function(){api.setMode(api.Mode.InsertHighlightAnnotation);}
                    },
                    {
                        text: "Insert",
                        value: function(){api.setMode(api.Mode.InsertCaretAnnotation);}
                    },
                    {
                        text: "Oval",
                        value: function(){api.setMode(api.Mode.InsertCircleAnnotation);}
                    },
                    {
                        text: "Rectangle",
                        value: function(){api.setMode(api.Mode.InsertSquareAnnotation);}
                    },
                    {
                        text: "Strikeout",
                        value: function(){api.setMode(api.Mode.InsertStrikeoutAnnotation);}
                    },
                    {
                        text: "Underline",
                        value: function(){api.setMode(api.Mode.InsertUnderlineAnnotation);}
                    }
                ]
            },
            {
                text: "Pages",
                items: [
                    {
                        text: "Append PDF",
                        value: api.append
                    },
                    {
                        text: "Move Page",
                        value: api.getPageViewed().movePage //show dialog
                    },
                    {
                        text: "Delete Page",
                        value: function(){api.getPageViewed().deletePage(true);} //show confirmation dialog
                    },
                    {
                        text: "Rotate Left",
                        value: function(){api.getPageViewed().rotatePage(-90);}
                    },
                    {
                        text: "Rotate Right",
                        value: function(){api.getPageViewed().rotatePage(90);}
                    },
                    {
                        text: "Crop",
                        value: function(){api.setMode(api.Mode.CropPage);}
                    },
                    {
                        text: "Deskew",
                        value: function(){api.setMode(api.Mode.DeskewPage);}
                    }
                ]
            }
        ],
        openOnClick: true,
        select: kendoOnSelectRunValue
    }).data("kendoMenu");

    // Add ToolBar
    var toolbar = $("#kendo-toolbar").kendoToolBar({
        resizable: true,
        items: [
            {
                type: "button",
                id: "ToolBarPointer",
                imageUrl: "images/pointer.png",
                click: function()
                    {
                        api.setMode(api.Mode.None);
                    }
            },
            {
                type: "button",
                id: "ToolBarSelectText",
                imageUrl: "images/text.png",
                click: function()
                    {
                        api.setMode(api.Mode.SelectText);
                    }
            },
            { type: "separator" },
            {
                type: "buttonGroup",
                buttons: [
                    { text: "Save", click: api.save },
                    { text: "Download", click: api.download },
                    { text: "Print", click: api.print }
                ]
            },
            { type: "separator" },
            {
                type: "buttonGroup",
                buttons: [
                    {
                        id: "ToolBarPreviousPage",
                        text: "Previous Page", 
                        click: function()
                            {
                                api.setView(
                                    {
                                        page: Math.max(api.getView().page - 1, 1),
                                        scrollY: 0
                                    });
                            }
                    },
                    {
                        id: "ToolBarNextPage",
                        text: "Next Page", 
                        click: function()
                            {
                                api.setView(
                                    {
                                        page: Math.min(api.getView().page + 1, api.getPageCount()),
                                        scrollY: 0
                                    });
                            }
                    }
                ]
            },
            { type: "separator" },
            {
                type: "buttonGroup",
                buttons: [
                    {
                        id: "ToolBarZoomOut",
                        text: "-", 
                        click: function()
                            {
                                api.setView(
                                    {
                                        zoom: Math.max(Math.floor(api.getView().zoom * 0.8), 25)
                                    });
                            }
                    },
                    {
                        id: "ToolBarZoomFit",
                        text: "Fit", 
                        click: function()
                            {
                                api.setView(
                                    {
                                        zoom: api.ViewZoom.ZoomFitAll
                                    });
                            }
                    },
                    {
                        id: "ToolBarZoom100",
                        text: "100%", 
                        click: function()
                            {
                                api.setView(
                                    {
                                        zoom: api.ViewZoom.Zoom100
                                    });
                            }
                    },
                    {
                        id: "ToolBarZoomIn",
                        text: "+", 
                        click: function()
                            {
                                api.setView(
                                    {
                                        zoom: Math.min(Math.floor(api.getView().zoom * 1.25), 500)
                                    });
                            }
                    }
                ]
            },
            { type: "separator" },
            {
                type: "buttonGroup",
                buttons: [
                    {
                        id: "ToolBarUndo",
                        text: "Undo",
                        click: function() { api.undo(); }
                    },
                    {
                        id: "ToolBarRedo",
                        text: "Redo",
                        click: function() { api.redo(); }
                    }
                ]
            }
        ]
    }).data("kendoToolBar");

    // Add handlers to RAD PDF
    api.addEventListener("modeChanged", function(evt)
    {
        $("#kendo-toolbar").find("#ToolBarPointer").css("display", (api.Mode.None == evt.mode) ? "none" : "");
        $("#kendo-toolbar").find("#ToolBarSelectText").css("display", (api.Mode.None == evt.mode) ? "" : "none");
    });
    api.addEventListener("objectAdded", function(evt)
    {
        // For all object types except ink shapes and popup annotations, disable the active tool after adding an object.
        switch(evt.obj.getType())
        {
            case api.ObjectType.InkShape:
            case api.ObjectType.PopupAnnotation:
                // Ignore
                break;

            default:
                api.setMode(api.Mode.None);
                break;
        }
    });
    api.addEventListener("objectSelect", function(evt)
    {
        var isLocked = false;

        switch (evt.obj.getType())
        {
            case api.ObjectType.ButtonField:
            case api.ObjectType.CheckField:
            case api.ObjectType.ComboField:
            case api.ObjectType.ListField:
            case api.ObjectType.RadioField:
            case api.ObjectType.TextField:
                isLocked = evt.obj.isLocked();
                break;
        }

        menu.enable($("#kendo-menu").find(".MenuEditDelete"), !isLocked);
        menu.enable($("#kendo-menu").find(".MenuEditProperties"), !isLocked);
        menu.enable($("#kendo-menu").find(".MenuEditUnlock"), isLocked);

        menu.enable($("#kendo-menu").find(".MenuEdit"), true);
    });
    api.addEventListener("objectUnselect", function(evt)
    {
        menu.enable($("#kendo-menu").find(".MenuEdit"), false);
    });
    api.addEventListener("undoChanged", function(evt)
    {
        toolbar.enable($("#kendo-toolbar").find("#ToolBarUndo"), !!evt.undoStackLength);
        toolbar.enable($("#kendo-toolbar").find("#ToolBarRedo"), !!evt.redoStackLength);
    });
    api.addEventListener("viewChanged", function(evt)
    {
        toolbar.enable($("#kendo-toolbar").find("#ToolBarPreviousPage"), api.getPageViewed().getPageNumber() > 1);
        toolbar.enable($("#kendo-toolbar").find("#ToolBarNextPage"), api.getPageViewed().getPageNumber() < api.getPageCount());
    });

    // Init Pointer / Select Text
    $("#kendo-toolbar").find("#ToolBarPointer").css("display", "none");

    // Init Page buttons
    toolbar.enable($("#kendo-toolbar").find("#ToolBarPreviousPage"), api.getPageViewed().getPageNumber() > 1);
    toolbar.enable($("#kendo-toolbar").find("#ToolBarNextPage"), api.getPageViewed().getPageNumber() < api.getPageCount());

    // Init Undo buttons
    toolbar.enable($("#kendo-toolbar").find("#ToolBarUndo"), false);
    toolbar.enable($("#kendo-toolbar").find("#ToolBarRedo"), false);

    // Show RAD PDF
    $("#radpdf").show();

    // Resize RAD PDF with window resizing
    radpdfOnResize();
    $(window).resize(radpdfOnResize);
}