import * as alt from 'alt-client';
import * as game from "natives";

let LoginRegisterBrowser;

alt.onServer('LoginRegister:Create', () => {
    LoginRegisterBrowser = new alt.WebView("http://resource/Alfheim_Client/Register_Login/main.html");
    LoginRegisterBrowser.focus();
    alt.showCursor(true);
    alt.toggleGameControls(false);
    LoginRegisterBrowser.on('Window:LoginClicked', (name, password) => {
        alt.emitServer('Alfheim:Login', name, password);
        alt.log("Alfheim:Login " + name + " | " + password);
    });
    LoginRegisterBrowser.on('Window:RegisterClicked', (name, password) => {
        alt.emitServer('Alfheim:Register', name, password);
        alt.log("Alfheim:Register " + name + " | " + password);
    });
});