import { initializeEvergineBase } from "evergine-react";
import { appStore } from "evergine/app-store";
import {
  EVERGINE_ASSEMBLY_NAME,
  EVERGINE_CLASS_NAME,
  EVERGINE_LOADING_BAR_ID,
} from "evergine/config";

declare global {
  let Blazor: { start(): Promise<void> };

  interface AppEventsListener {
    onCameraClick: (rotation: number) => void;
  }

  interface WebEventsProxy {
    changeColor: (color: string) => void;
    setTestMode: (activated: boolean) => void;
  }
}

function addCustomEvents() {
  window.App.appEventsListener.onCameraClick = (rotation: number): void => {
    appStore.setState({ cameraClick: { rotation } });
  };

  window.App.webEventsProxy.changeColor = (color: string): void => {
    window.Utils.invoke("ChangeColor", color);
  };

  window.App.webEventsProxy.setTestMode = (activated: boolean): void => {
    window.Utils.invoke("SetTestMode", activated);
  };
}

const initializeEvergine = (): void => {
  initializeEvergineBase(
    EVERGINE_LOADING_BAR_ID,
    EVERGINE_ASSEMBLY_NAME,
    EVERGINE_CLASS_NAME,
    addCustomEvents
  );
};

export { initializeEvergine };
