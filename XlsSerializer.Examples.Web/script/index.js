var app = app || {};
app.index = app.index || {};
app.index.load = app.index.load || function () {
    
    var replaceTag = function(template, tag, value) {
        var parts = template.split(tag);
        return parts.join(value);
    };

    var renderIndex = function(index) {
        var leftMenu = $(".index");
        var itemTemplateHtml = leftMenu.find(".indexItem")[0].outerHTML;
        
        var urlParams = new URLSearchParams(window.location.search);

        var currentKey = null;

        for (const [key, value] of urlParams) {
            currentKey = currentKey || key;
            if (key) {
                break;
            }
        }

        currentKey = currentKey || index[0].Key;

        var template = new ElementTemplate(".index>.indexItem");

        for (var i = 0; i < index.length; i++) {
            var dataItem = index[i];

            var newClass = "";
            if (dataItem.Key === currentKey) {
                newClass = "currentItem";
            }

            template.render(leftMenu[0], { "%link%": "?" + dataItem.Key, "%title%": dataItem.Title, "hidden": newClass });
        }

        $.get("/data/" + currentKey + ".json", app.content.load);
    };

    var receiveIndex = function(indexData) {
        renderIndex(indexData);
    };

    $.get("/data/index.json", receiveIndex);
};


app.index.load();