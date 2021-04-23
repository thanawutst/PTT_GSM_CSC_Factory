import "./Styles/bootstrap/dist/css/bootstrap.css";
import * as React from "react";
import * as ReactDOM from "react-dom";
import { i18n, init as i18nInit } from "src/i18n";
import App from "./App";
import "src/fonts/styles/MuiFontIcons.css";
import registerServiceWorker, { unregister } from "./registerServiceWorker";

(async function () {
  await i18nInit();
  document.title = i18n("app.title");
  ReactDOM.render(<App />, document.getElementById("root"));

  unregister();
})();
