import React, { Component } from "react";
import { Grid, Typography } from "@material-ui/core";
import FroalaEditor from "react-froala-wysiwyg";
import 'froala-editor/css/froala_style.min.css';
import 'froala-editor/css/froala_editor.pkgd.min.css';
import Tag from "reactstrap/lib/Tag";
import { useFormContext } from 'react-hook-form';

var tmp = document.createElement("div");
let validateWord;

const handleCountingWords = (text) => {
    let textReplaceComma = text.replace(",", ", ");
    let textReplaceDot = textReplaceComma.replace(".", ". ");
    let arrWord = textReplaceDot.trim().split(" ");

    //arrWord = arrWord.remove(" ");
    //arrWord = arrWord.remove(",");
    //arrWord = arrWord.remove(".");

    return arrWord.length;
};
const state = {
    config: {
        placeholderText: "Edit Your Content Here!",
        attribution: false,
        heightMin: 200,
        theme: "gray",
        imageUpload: false,
        fileUpload: false,
        videoUpload: false,
        charCounterCount: true,
        pasteDeniedTags: [
            "a",
            "abbr",
            "acronym",
            "address",
            "applet",
            "area",
            "article",
            "aside",
            "audio",
            "b",
            "base",
            "basefont",
            "bdi",
            "bdo",
            "bgsound",
            "big",
            "blink",
            "blockquote",
            "body",
            "br",
            "button",
            "canvas",
            "caption",
            "center",
            "cite",
            "code",
            "col",
            "colgroup",
            "content",
            "data",
            "datalist",
            "dd",
            "decorator",
            "del",
            "details",
            "dfn",
            "dir",
            "div",
            "dl",
            "dt",
            "element",
            "em",
            "embed",
            "fieldset",
            "figcaption",
            "figure",
            "font",
            "footer",
            "form",
            "frame",
            "frameset",
            "h1",
            "h2",
            "h3",
            "h4",
            "h5",
            "h6",
            "head",
            "header",
            "hgroup",
            "hr",
            "html",
            "i",
            "iframe",
            "img",
            "input",
            "ins",
            "isindex",
            "kbd",
            "keygen",
            "label",
            "legend",
            "li",
            "link",
            "listing",
            "main",
            "map",
            "mark",
            "marquee",
            "menu",
            "menuitem",
            "meta",
            "meter",
            "nav",
            "nobr",
            "noframes",
            "noscript",
            "object",
            "ol",
            "optgroup",
            "option",
            "output",
            "p",
            "param",
            "plaintext",
            "pre",
            "progress",
            "q",
            "rp",
            "rt",
            "ruby",
            "s",
            "samp",
            "script",
            "section",
            "select",
            "shadow",
            "small",
            "source",
            "spacer",
            "span",
            "strike",
            "strong",
            "style",
            "sub",
            "summary",
            "sup",
            "table",
            "tbody",
            "td",
            "template",
            "textarea",
            "tfoot",
            "th",
            "thead",
            "time",
            "title",
            "tr",
            "track",
            "tt",
            "u",
            "ul",
            "var",
            "video",
            "wbr",
            "xmp",
        ],
        pasteDeniedAttrs: ["class", "id", "style", "data-.*"],
        fontFamily: {
            Arial: "Arial",
        },
        imagePaste: false,
        fontFamilyDefaultSelection: "Arial",
        fontFamilySelection: true,
        wordDeniedAttrs: ["style"],
        toolbarButtons: {
            moreText: {
                buttons: [
                    "bold",
                    "italic",
                    "underline",
                    "strikeThrough",
                    "subscript",
                    "superscript",
                    "fontFamily",
                    "fontSize",
                    "textColor",
                    "backgroundColor",
                    "inlineClass",
                    "inlineStyle",
                    "clearFormatting",
                ],
            },
            moreParagraph: {
                buttons: [
                    "alignLeft",
                    "alignCenter",
                    "formatOLSimple",
                    "alignRight",
                    "alignJustify",
                    "formatOL",
                    "formatUL",
                    "paragraphFormat",
                    "paragraphStyle",
                    "lineHeight",
                    "outdent",
                    "indent",
                    "quote",
                    "insertLink",
                ],
            },
            moreRich: {
                buttons: [
                    "insertLink",
                    "insertTable",
                    "fontAwesome",
                    "specialCharacters",
                ],
            },
            moreMisc: {
                buttons: [
                    "undo",
                    "redo",
                    //"fullscreen",
                    //"print",
                    //"getPDF",
                    //"spellChecker",
                    "selectAll",
                    "html",
                    "help",
                ],
                align: "right",
                buttonsVisible: 2,
            },
        },
        quickInsertEnabled: false,
        key:
            "0BA3jA11B5C7C4A4D3aIVLEABVAYFKc2Cb1MYGH1g1NYVMiG5G4E3C3A1C8A6D4A3B4==", // Pass your key here
        events: {
            "paste.after": function () {
                // Do something here.
                // this is the editor instance.

            },
        },
    },
};
export function FroalaBasicMoreTools(props) {
    const {
        isCountingWord, // เมื่อใส่ words ถุกต้องจะ return true เช่น max = 5, min = 1 ใส่ model = 2
        labal, // ชื่อ label
        model, //  Froala Text Editer
        handleModelChange, // funcion ที่ใช้ในการรับค่าของ model
        max, // ค่า words ที่ใส่มากสุด
        min, // ค่า words ที่ใส่อย่างน้อย
        validateWords, // ส่ง true มาเพื่อนับ words | ส่ง false มาเพื่อไม่ต้องการนับ words
        id,
    } = props;

    const {
        register,
        errors,
        formState: { touched, isSubmitted },
        getValues
    } = useFormContext();

    tmp.innerHTML = model;

    const valuesString = model ? handleCountingWords(tmp.textContent) : 0;
    validateWord = valuesString >= min && valuesString <= max;
    isCountingWord === true && validateWords(validateWord);

    return (
        <div>
            <Grid>
                <Typography>{labal || "LABAL"}</Typography>
            </Grid>
            <Grid container spacing={1}>
                <Grid item xs={12} lg={12}>
                    <FroalaEditor
                        model={model}
                        onModelChange={handleModelChange}
                        config={state.config}

                    />
                </Grid>
                <Grid item xs={9} sm={11}></Grid>
            </Grid>
        </div>
    );
}

export default FroalaBasicMoreTools;
