'use strict';var vue=require('vue');function _slicedToArray(arr, i) {
    return _arrayWithHoles(arr) || _iterableToArrayLimit(arr, i) || _unsupportedIterableToArray(arr, i) || _nonIterableRest();
  }
  
  function _arrayWithHoles(arr) {
    if (Array.isArray(arr)) return arr;
  }
  
  function _iterableToArrayLimit(arr, i) {
    var _i = arr == null ? null : typeof Symbol !== "undefined" && arr[Symbol.iterator] || arr["@@iterator"];
  
    if (_i == null) return;
    var _arr = [];
    var _n = true;
    var _d = false;
  
    var _s, _e;
  
    try {
      for (_i = _i.call(arr); !(_n = (_s = _i.next()).done); _n = true) {
        _arr.push(_s.value);
  
        if (i && _arr.length === i) break;
      }
    } catch (err) {
      _d = true;
      _e = err;
    } finally {
      try {
        if (!_n && _i["return"] != null) _i["return"]();
      } finally {
        if (_d) throw _e;
      }
    }
  
    return _arr;
  }
  
  function _unsupportedIterableToArray(o, minLen) {
    if (!o) return;
    if (typeof o === "string") return _arrayLikeToArray(o, minLen);
    var n = Object.prototype.toString.call(o).slice(8, -1);
    if (n === "Object" && o.constructor) n = o.constructor.name;
    if (n === "Map" || n === "Set") return Array.from(o);
    if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return _arrayLikeToArray(o, minLen);
  }
  
  function _arrayLikeToArray(arr, len) {
    if (len == null || len > arr.length) len = arr.length;
  
    for (var i = 0, arr2 = new Array(len); i < len; i++) arr2[i] = arr[i];
  
    return arr2;
  }
  
  function _nonIterableRest() {
    throw new TypeError("Invalid attempt to destructure non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method.");
  }/* eslint-disable */
  
  /**
   * Gán sự kiện nhấn click chuột ra ngoài combobox data (ẩn data list đi)
   * NVMANH (31/07/2022)
   */
  var clickoutside = {
    mounted: function mounted(el, binding, vnode, prevVnode) {
      el.clickOutsideEvent = function (event) {
        // Nếu element hiện tại không phải là element đang click vào
        // Hoặc element đang click vào không phải là button trong combobox hiện tại thì ẩn đi.
        if (!(el === event.target || // click phạm vi của combobox__data
        el.contains(event.target) || //click vào element con của combobox__data
        el.previousElementSibling.contains(event.target) //click vào element button trước combobox data (nhấn vào button thì hiển thị)
        )) {
          // Thực hiện sự kiện tùy chỉnh:
          binding.value(event, el); // vnode.context[binding.expression](event); // vue 2
        }
      };
  
      document.body.addEventListener("click", el.clickOutsideEvent);
    },
    beforeUnmount: function beforeUnmount(el) {
      document.body.removeEventListener("click", el.clickOutsideEvent);
    }
  };
  
  function removeVietnameseTones(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D"); // Some system encode vietnamese combining accent as individual utf-8 characters
    // Một vài bộ encode coi các dấu mũ, dấu chữ như một kí tự riêng biệt nên thêm hai dòng này
  
    str = str.replace(/\u0300|\u0301|\u0303|\u0309|\u0323/g, ""); // ̀ ́ ̃ ̉ ̣  huyền, sắc, ngã, hỏi, nặng
  
    str = str.replace(/\u02C6|\u0306|\u031B/g, ""); // ˆ ̆ ̛  Â, Ê, Ă, Ơ, Ư
    // Remove extra spaces
    // Bỏ các khoảng trắng liền nhau
  
    str = str.replace(/ + /g, " ");
    str = str.trim(); // Remove punctuations
    // Bỏ dấu câu, kí tự đặc biệt
  
    str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g, " ");
    return str;
  }
  
  var keyCode = {
    Enter: 13,
    ArrowUp: 38,
    ArrowDown: 40,
    ESC: 27
  };
  var script = {
    name: "MSCombobox",
    directives: {
      clickoutside: clickoutside
    },
    props: {
      value: null,
      url: String,
      propValue: String,
      propText: String,
      isLoadData: {
        type: Boolean,
        default: true
      }
    },
    methods: {
      /**
       * Lưu lại index của item đã focus
       * NVMANH (31/07/2022)
       */
      saveItemFocus: function saveItemFocus(index) {
        this.indexItemFocus = index;
      },
  
      /**
       * Ẩn danh sách item
       * NVMANH (31/07/2022)
       */
      hideListData: function hideListData() {
        this.isShowListData = false;
      },
  
      /**
       * Nhấn vào button thì hiển thị hoặc ẩn List Item
       * NVMANH (31/07/2022)
       */
      btnSelectDataOnClick: function btnSelectDataOnClick() {
        this.dataFilter = this.data;
        this.isShowListData = !this.showListData;
      },
  
      /**
       * Click chọn item trong danh sách
       * NVMANH (31/07/2022)
       */
      itemOnSelect: function itemOnSelect(item, index) {
        var text = item[this.propText];
        var value = item[this.propValue];
        this.textInput = text; // Hiển thị text lên input.
  
        this.indexItemSelected = index;
        this.isShowListData = false;
        this.$emit("getValue", value, text, item);
      },
  
      /**
       * Nhập text thì thực hiện filter dữ liệu và hiển thị
       * NVMANH (31/07/2022)
       */
      inputOnChange: function inputOnChange() {
        var me = this; // Thực hiện lọc các phần tử phù hợp trong data:
  
        this.dataFilter = this.data.filter(function (e) {
          var text = removeVietnameseTones(me.textInput).toLowerCase().replace(" ", "");
          var textOfItem = removeVietnameseTones(e[me.propText]).toLowerCase().replace(" ", "");
          return textOfItem.includes(text);
        });
        this.isShowListData = true;
      },
  
      /**
       * Lựa chọn item bằng cách nhấn các phím lên, xuống trên bàn phím
       * NVMANH (31/07/2022)
       */
      selecItemUpDown: function selecItemUpDown() {
        var me = this;
        var keyCodePress = event.keyCode;
        var elToFocus = null;
  
        switch (keyCodePress) {
          case keyCode.ESC:
            this.isShowListData = false;
            break;
  
          case keyCode.ArrowDown:
            this.isShowListData = true;
            elToFocus = this.$refs["toFocus_".concat(me.indexItemFocus + 1)];
  
            if (this.indexItemFocus == null || !elToFocus || elToFocus.length == 0) {
              this.indexItemFocus = 0;
            } else {
              this.indexItemFocus += 1;
            }
  
            break;
  
          case keyCode.ArrowUp:
            this.isShowListData = true;
            elToFocus = this.$refs["toFocus_".concat(me.indexItemFocus - 1)];
  
            if (this.indexItemFocus == null || !elToFocus || elToFocus.length == 0) {
              this.indexItemFocus = this.dataFilter.length - 1;
            } else {
              this.indexItemFocus -= 1;
            }
  
            break;
  
          case keyCode.Enter:
            elToFocus = this.$refs["toFocus_".concat(me.indexItemFocus)];
  
            if (elToFocus && elToFocus.length > 0) {
              elToFocus[0].click();
              this.isShowListData = false;
            }
  
            break;
        }
      }
    },
    created: function created() {
      var _this = this;
  
      // Thực hiện lấy dữ liệu từ api:
      if (this.url) {
        fetch(this.url).then(function (res) {
          return res.json();
        }).then(function (res) {
          _this.data = res;
          _this.dataFilter = res;
        }).catch(function (res) {
          console.log(res);
        });
      }
    },
    data: function data() {
      return {
        data: [],
        // data gốc
        textInput: null,
        dataFilter: [],
        // data đã được filter
        isShowListData: false,
        // Hiển thị list data hay không
        indexItemFocus: null,
        // Index của item focus hiện tại
        indexItemSelected: null // Index của item được selected
  
      };
    }
  };var _hoisted_1 = {
    class: "combobox"
  };
  var _hoisted_2 = ["src"];
  var _hoisted_3 = {
    key: 0,
    class: "combobox__data",
    ref: "combobox__data"
  };
  var _hoisted_4 = ["value", "onClick", "onFocus", "onKeydown", "onKeyup"];
  var _hoisted_5 = {
    class: "combobox__item-icon"
  };
  var _hoisted_6 = ["src"];
  function render(_ctx, _cache, $props, $setup, $data, $options) {
    var _this = this;
  
    var _directive_clickoutside = vue.resolveDirective("clickoutside");
  
    return vue.openBlock(), vue.createElementBlock("div", _hoisted_1, [vue.withDirectives(vue.createElementVNode("input", {
      type: "text",
      class: "input combobox__input",
      "onUpdate:modelValue": _cache[0] || (_cache[0] = function ($event) {
        return $data.textInput = $event;
      }),
      onInput: _cache[1] || (_cache[1] = function () {
        return $options.inputOnChange && $options.inputOnChange.apply($options, arguments);
      }),
      onKeydown: _cache[2] || (_cache[2] = function () {
        return $options.selecItemUpDown && $options.selecItemUpDown.apply($options, arguments);
      })
    }, null, 544), [[vue.vModelText, $data.textInput]]), vue.createElementVNode("button", {
      class: "button combobox__button",
      onClick: _cache[3] || (_cache[3] = function () {
        return $options.btnSelectDataOnClick && $options.btnSelectDataOnClick.apply($options, arguments);
      }),
      onKeydown: _cache[4] || (_cache[4] = function () {
        return $options.selecItemUpDown && $options.selecItemUpDown.apply($options, arguments);
      }),
      tabindex: "-1"
    }, [vue.createElementVNode("img", {
      src: require('../src/icon/down.png'),
      alt: "",
      srcset: "",
      width: "24",
      height: "24"
    }, null, 8, _hoisted_2)], 32), $data.isShowListData ? vue.withDirectives((vue.openBlock(), vue.createElementBlock("div", _hoisted_3, [(vue.openBlock(true), vue.createElementBlock(vue.Fragment, null, vue.renderList($data.dataFilter, function (item, index) {
      return vue.openBlock(), vue.createElementBlock("a", {
        class: vue.normalizeClass(["combobox__item", {
          'combobox__item--focus': index == $data.indexItemFocus,
          'combobox__item--selected': index == $data.indexItemSelected
        }]),
        key: item[_this.propValue],
        ref_for: true,
        ref: 'toFocus_' + index,
        value: item[_this.propValue],
        onClick: function onClick($event) {
          return $options.itemOnSelect(item, index);
        },
        onFocus: function onFocus($event) {
          return $options.saveItemFocus(index);
        },
        onKeydown: function onKeydown($event) {
          return $options.selecItemUpDown(index);
        },
        onKeyup: function onKeyup($event) {
          return $options.selecItemUpDown(index);
        },
        tabindex: "1"
      }, [vue.createElementVNode("div", _hoisted_5, [vue.withDirectives(vue.createElementVNode("img", {
        src: require('../src/icon/check.png'),
        alt: "",
        srcset: "",
        width: "14",
        height: "11"
      }, null, 8, _hoisted_6), [[vue.vShow, index == $data.indexItemSelected]])]), vue.createElementVNode("span", null, vue.toDisplayString(item[_this.propText]), 1)], 42, _hoisted_4);
    }), 128))])), [[_directive_clickoutside, $options.hideListData]]) : vue.createCommentVNode("", true)]);
  }function styleInject(css, ref) {
    if ( ref === void 0 ) ref = {};
    var insertAt = ref.insertAt;
  
    if (!css || typeof document === 'undefined') { return; }
  
    var head = document.head || document.getElementsByTagName('head')[0];
    var style = document.createElement('style');
    style.type = 'text/css';
  
    if (insertAt === 'top') {
      if (head.firstChild) {
        head.insertBefore(style, head.firstChild);
      } else {
        head.appendChild(style);
      }
    } else {
      head.appendChild(style);
    }
  
    if (style.styleSheet) {
      style.styleSheet.cssText = css;
    } else {
      style.appendChild(document.createTextNode(css));
    }
  }var css_248z = "\n.combobox[data-v-233bc0ac] {\n  position: relative;\n  /* flex-direction: row; */\n  border-radius: 4px;\n  box-sizing: border-box;\n}\n.combobox__input[data-v-233bc0ac],\nselect[data-v-233bc0ac] {\n  width: 100%;\n  height: 40px;\n  flex: 1;\n  padding-right: 56px !important;\n  padding-left: 16px;\n  border-radius: 4px;\n  outline: none;\n  border: 1px solid #bbbbbb;\n  font-family: MISA Fonts;\n  box-sizing: border-box;\n}\n.combobox__input[data-v-233bc0ac]:focus,\n.combobox__input:focus ~ .combobox__button[data-v-233bc0ac] {\n  border-color: #019160;\n}\n.combobox__button[data-v-233bc0ac] {\n  position: absolute;\n  display: flex;\n  align-items: center;\n  justify-content: center;\n  color: rgb(90, 90, 90);\n  border-radius: 0 4px 4px 0;\n  right: 0px;\n  top: 0px;\n  border: 1px solid #bbbbbb;\n  border-left: unset;\n  height: 40px;\n  width: 40px;\n  border-radius: 0 4px 4px 0px;\n  background-color: #fff;\n  cursor: pointer;\n  min-width: unset !important;\n  outline: none;\n  box-sizing: border-box;\n  opacity: 0.5;\n}\n.combobox__button[data-v-233bc0ac]:hover,\n.combobox__button[data-v-233bc0ac]:focus {\n  background-color: #bbbbbb;\n  color: #000;\n}\n.combobox__data[data-v-233bc0ac] {\n  display: flex;\n  flex-direction: column;\n  padding: 4px 0px;\n  position: absolute;\n  width: 100%;\n  top: 100%;\n  left: 0;\n  border-radius: 4px;\n  background-color: #fff;\n  box-shadow: 0px 3px 6px #00000016;\n  z-index: 999;\n}\n.combobox__item[data-v-233bc0ac] {\n  display: flex;\n  align-items: center;\n  justify-content: flex-start;\n  line-height: 40px;\n  padding-left: 10px;\n  height: 40px;\n  cursor: pointer;\n  /* border: 1px solid #ccc; */\n  outline: none;\n}\n.combobox__item-icon[data-v-233bc0ac] {\n  width: 16px;\n  height: 16px;\n  font-size: 16px;\n  margin-right: 10px;\n  display: flex;\n  align-items: center;\n  justify-content: center;\n}\n.combobox__item-icon--selected[data-v-233bc0ac] {\n  width: 14px;\n  height: 11px;\n}\n.combobox__item[data-v-233bc0ac]:hover,\n.combobox__item[data-v-233bc0ac]:focus,\n.combobox__item--focus[data-v-233bc0ac],\n.combobox__item--hover[data-v-233bc0ac] {\n  background-color: #e9ebee;\n  color: #000;\n}\n.combobox__item--selected[data-v-233bc0ac] {\n  pointer-events: none;\n  background-color: #019160;\n  color: #fff;\n}\n";
  styleInject(css_248z);script.render = render;
  script.__scopeId = "data-v-233bc0ac";// Import vue component
  // IIFE injects install function into component, allowing component
  // to be registered via Vue.use() as well as Vue.component(),
  
  var component = /*#__PURE__*/(function () {
    // Get component instance
    var installable = script; // Attach install function executed by Vue.use()
  
    installable.install = function (app) {
      app.component('MsCombobox', installable);
    };
  
    return installable;
  })(); // It's possible to expose named exports when writing components that can
  // also be used as directives, etc. - eg. import { RollupDemoDirective } from 'rollup-demo';
  // export const RollupDemoDirective = directive;
  var namedExports=/*#__PURE__*/Object.freeze({__proto__:null,'default':component});// only expose one global var, with named exports exposed as properties of
  // that global var (eg. plugin.namedExport)
  
  Object.entries(namedExports).forEach(function (_ref) {
    var _ref2 = _slicedToArray(_ref, 2),
        exportName = _ref2[0],
        exported = _ref2[1];
  
    if (exportName !== 'default') component[exportName] = exported;
  });module.exports=component;