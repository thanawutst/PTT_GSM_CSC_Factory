import * as React from "react";
import { apiurl, url } from "src/Service/Config/ApiUrl";

const $ = window.jQuery;
export const Extension = {
	Image: ['jpg', 'jpeg', 'png', 'gif'],
	Video: ['mov', 'wmv', 'avi', 'mp4'],
	PDF: ['pdf'],
	Document: ['doc', 'docx', 'xls', 'xlsx', 'txt'],
	Word: ["doc", "docx"],
	Excel: ["xls", "xlsx"],
	Powpoint: ["pptx", "pdf", "ppt"],
	txt: ["txt"],
	Email: ["msg"],
	Other: ['rar', 'zip'],
	File: ['jpg', 'jpeg', 'png', 'gif', 'pdf', 'doc', 'docx', 'xls', 'xlsx', 'txt', "pptx", "ppt", 'rar', 'zip'],
	AllType: null
};

class Fileuploader extends React.Component {
	constructor(props) {
		super(props);
		const sPath = apiurl + "/UploadFileToTemp";

		this.state = {
			name: "files",
			options: {
				upload: {
					url: sPath,
				}
			}
		};
		this.state.options['upload'] = {
			url: sPath,
			beforeSend: function (item, listEl, parentEl, newInputEl, inputEl) {
				return true;
			},
		}
		this.state.lst = [];
		this.state.options['editor'] = true;
		let sFileType = "";
		this.state.sextensions = "";
		this.state.maxSize = 50;
		if (this.props.extensions != null) {
			$.each(this.props.extensions, function () {
				sFileType += " / " + this;
			});
		}
		else {
			sFileType = "/.gif /.jpeg / .jpg / .txt / .doc / .docx / .xls / .xlsx / .pdf / .pptx / .ppt / .mp4 / .rar / .zip / .msg ";
		}
		this.state.sextensions = sFileType;
	}
	SetUploadFile() {
		let onComplete = this.props.onComplete;
		let onRemoveComplete = this.props.onRemoveComplete;
		let multiFile = this.props.multiFile;
		this.$el = $(this.el);
		let arrFile = this.state.lst;
		const sPath = apiurl + "/UploadFileToTemp";
		var fileList = this.props.fileList;
		var ThisIsRefProp = this
		var multiFileList = [];
		this.$el.fileuploader($.extend({
			enableApi: true,
			limit: this.props.limit,

			maxSize: null,
			fileMaxSize: this.props.fileMaxSize,
			extensions: this.props.extensions,
			files: null,
			changeInput: '<div class="fileuploader-input">' +
				'<div class="fileuploader-input-caption">' +
				'<span>${captions.feedback}</span>' +
				'</div>' +
				'<div class="fileuploader-input-button">' +
				'<span>${captions.button}</span>' +
				'</div>' +
				'</div><div class="form-group row col-12 dvValidate">' +
				'<div>' +
				'<span class="text-red">File size limits up to ' + this.props.fileMaxSize + ' MB. Allowed file types: ' + this.state.sextensions + '</span><br/>' +
				'</div>' +
				'</div>',
			upload: {
				url: sPath,
				beforeSend: function (item, listEl, parentEl, newInputEl, inputEl) {
					return true;
				},
				onSuccess: function (data, item, listEl, parentEl, newInputEl, inputEl, textStatus, jqXHR) {
					item.html.find('.fileuploader-action-remove').addClass('fileuploader-action-success');

					setTimeout(function () {
						item.html.find('.progress-bar2').fadeOut(400);
					}, 400);

					// data.sSize = item.size + "";
					let nID = 1;
					let lstFile = arrFile;
					if (lstFile.length > 0) {
						nID = lstFile.reduce(
							(max, lstFile) => (lstFile.nID > max ? lstFile.nID : max),
							lstFile[0].nID
						);
					}
					item.url = url + data.sSaveToPath + '/' + data.sSaveToFileName;

					multiFileList = ThisIsRefProp.props.fileList;

					if (multiFile != true) {
						fileList.length = 0
						ThisIsRefProp.props.fileList.push(data)
					}
					else {
						multiFileList.push(data)
					}
				},
				onComplete: function (listEl, parentEl, newInputEl, inputEl, jqXHR, textStatus) {

					if (multiFile != true) {
						ThisIsRefProp.props.fileList.forEach(e => fileList.push(e))
						// onComplete()
						onComplete(fileList)
					} else {
						let arr = [];
						let found = [];

						multiFileList.forEach(el => {
							found = arr.find(item => item.sSaveToFileName == el.sSaveToFileName);
							if (!found) {
								arr.push(el);
							}
						});
						onComplete(arr)
					}
				},
				onError: function (item, listEl, parentEl, newInputEl, inputEl, jqXHR, textStatus, errorThrown) {
					console.log('onerror =>')
					var progressBar = item.html.find('.progress-bar2');

					if (progressBar.length > 0) {
						progressBar.find('span').html(0 + "%");
						progressBar.find('.fileuploader-progressbar .bar').width(0 + "%");
						item.html.find('.progress-bar2').fadeOut(400);
					}
					if (item.upload.status !== 'cancelled' && item.html.find('.fileuploader-action-retry').length === 0) {
						item.html.find('.column-actions').prepend('<a class="fileuploader-action fileuploader-action-retry" title="Retry"><i></i></a>')
					}
					// item.upload.status !== 'cancelled' && item.html.find('.fileuploader-action-retry').length === 0 ? item.html.find('.column-actions').prepend('<a class="fileuploader-action fileuploader-action-retry" title="Retry"><i></i></a>') : null;
				},
				onProgress: function (data, item, listEl, parentEl, newInputEl, inputEl) {
					var progressBar = item.html.find('.progress-bar2');

					if (progressBar.length > 0) {
						progressBar.show();
						progressBar.find('span').html(data.percentage + "%");
						progressBar.find('.fileuploader-progressbar .bar').width(data.percentage + "%");
					}
				},

			},
			onRemove: function (item, listEl, parentEl, newInputEl, inputEl) {
				if (onRemoveComplete) {
					if (multiFile != true) {
						var index;
						for (var x = 0; x < fileList.length; x++) {
							if (ThisIsRefProp.props.fileList[x].sFileName === item.name && Number(ThisIsRefProp.props.fileList[x].sSize) === item.size)
								index = x;
						}

						ThisIsRefProp.props.fileList.splice(index, 1)
						// onRemoveComplete()
						onRemoveComplete(ThisIsRefProp.props.fileList)
					} else {

						multiFileList = ThisIsRefProp.props.fileList;

						let arr = [];
						let found = [];
						multiFileList = multiFileList.filter(a => a.sFileName != item.name)

						multiFileList.forEach(el => {
							found = arr.find(item => item.sSaveToFileName == el.sSaveToFileName);
							if (!found) {
								arr.push(el);
							}
						});
						onRemoveComplete(arr)
					}
				}

			},
		}, {
		}));
		this.elment = $.fileuploader.getInstance(this.$el);

		this.api = $.fileuploader.getInstance(this.$el);
		let objtest = this.api;
		this.API_Obj = this.api;
	}
	componentDidMount() {
		console.log('didMount =>')
		this.SetUploadFile()
	};

	componentWillReceiveProps(nextProps) {
		if (this.props.fileList !== nextProps.fileList) {
			this.api.reset();
			nextProps.fileList.forEach(e => {
				if (e.sSaveToFileName !== "") {
					let arrfilename = (e.sSaveToFileName + "").split('.');
					e.sFileType = arrfilename[arrfilename.length - 1];
					let format = "application"
					let sType = format;
					if (Extension.Image.indexOf(e.sFileType) > -1) {
						format = "image";
					}
					if (Extension.Video.indexOf(e.sFileType) > -1) {
						format = 'video';
					}
					sType = format + "/" + e.sFileType;
					e.format = format;
					e.sFileType = sType;
					e.name = e.sFileName
					e.file = url + e.sSaveToPath + e.sSaveToFileName
					e.type = e.sFileType
					e.size = e.sSize
					e.url = url + e.sSaveToPath + e.sSaveToFileName;
					this.api.append(e)
				}

			})
		}
		if (this.props.readOnly == true) {
			$(".fileuploader-input").hide();
			$("fileuploader-list-files").hide();
			$(".dvValidate").hide();
			$(".btn-remove").hide();
		}
	}

	componentWillUnmount() {
		if (this.api)
			this.api.destroy();
	}

	render() {
		return (
			<div>
				<input
					type="file"
					name={this.state.name}
					ref={el => this.el = el}
				/>
			</div>
		)
	}
}

export default Fileuploader;
