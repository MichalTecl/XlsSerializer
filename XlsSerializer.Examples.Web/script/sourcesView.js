var app = app || {};
app.SourcesList = app.SourcesList || function() {

    var self = this;

    var sourceFiles = {};

    this.showFile = function(fileName) {
        var tabs = $("#sourceFileList>.sourceFileListItem");

        for (var i = 0; i < tabs.length; i++) {
            var elementFile = tabs[i].getAttribute("data-filename");

            if (elementFile === fileName) {
                $(tabs[i]).addClass("selectedItem");
            } else {
                $(tabs[i]).removeClass("selectedItem");
            }
        }

        $("#sourceFileContent").html(PR.prettyPrintOne(sourceFiles[fileName]));
    };

    this.initialize = function(files) {

        var list = $("#sourceFileList")[0];
        var template = new ElementTemplate("#sourceFileList>.sourceFileListItem");

        var defaultFile = null;

        for (var i = 0; i < files.length; i++) {
            var file = files[i];

            sourceFiles[file.Name] = file.Content;

            template.render(list,
                {
                    "%fileName%": file.Name,
                    "hidden":""
                });

            defaultFile = defaultFile || file.Name;

            if (file.IsDefault) {
                defaultFile = file.Name;
            }
        }

        self.showFile(defaultFile);
    };
};

app.sourcesList = app.sourcesList || new app.SourcesList();

