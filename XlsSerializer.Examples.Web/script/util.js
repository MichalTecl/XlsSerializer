var ElementTemplate = ElementTemplate || function(selector) {

    var html = $(selector)[0].outerHTML;

    this.render = function(container, replacements) {

        var code = html;

        for (var key in replacements) {
            var replacement = replacements[key];

            code = code.split(key).join(replacement);
        }

        container.innerHTML += code;
    };

};