

document.addEventListener("alpine:init", () => {
    Alpine.data("Dirs", () => ({
        _setting: {
            baseUrl: '/filemanager',
            ajaxParam: {
                cmd: '',
                value: '',
                secondaryValue: '',
            },
            setParams(cmd, value = '', secondaryValue = '') {
                this.ajaxParam.cdm = cmd;
                this.ajaxParam.value = value;
                this.ajaxParam.secondaryValue = secondaryValue;

            },
            getUrl() {
                return `${this.baseUrl}?${new URLSearchParams(this.ajaxParam)}`;
            }
        },
        _folderTree: [
            {
                level: 1,
                fullPath: '',
                folderName: '',
                isOpen: true,
                cssClass: {},
            }
        ],

        init() {
            this.refreshData();
        },
        refreshData() {
            this._setting.setParams("GET_ALL_DIR");
            fetch(this._setting.baseUrl)
                .then(x => x.json())
                .then(json => {
                    this._folderTree = json.data.map(path => {
                        var tmArr = path.split("\\");
                        return {
                            level: tmArr.length,
                            fullPath: path,
                            folderName: tmArr[tmArr.length -1],
                            isOpen: false,
                            cssClass: {
                                [`folder-level-${tmArr.length}`]: true,
                                 show: false
                            }
                        }
                    });
                })
                .catch(err => {
                    console.log(err);
                });
        },
        openFolder(level) {
            var child = document.querySelector(`.folder-level-${level + 1}`);
            if (child) {
                
                child.classList.toggle("show");
            }

        },
        toggleFolder(idx) {
            console.log(this.$el);

            if (idx >= this._folderTree.length) {
                return;
            }

            this._folderTree[idx].isOpen = !this._folderTree[idx].isOpen;
            var currentLevel = this._folderTree[idx].level;

            this.openFolder(idx, currentLevel);
        },
        openFolder(idx, maxLevel) {
            var isOpen = this._folderTree[idx].isOpen;
            if (isOpen) {
                while (idx + 1 < this._folderTree.length && this._folderTree[idx + 1].level > maxLevel) {
                    if (maxLevel + 1 == this._folderTree[idx + 1].level) {
                        this._folderTree[idx + 1].cssClass.show = true;
                        if (this._folderTree[idx + 1].isOpen) {
                            this.openFolder(idx + 1, this._folderTree[idx +1].level);
                        }
                    }
                    idx++;
                }
            } else {
                while (idx + 1 < this._folderTree.length && this._folderTree[idx + 1].level > maxLevel) {
                    this._folderTree[idx + 1].cssClass.show = false;
                    idx++;
                }
            }
        }





    }));
});
