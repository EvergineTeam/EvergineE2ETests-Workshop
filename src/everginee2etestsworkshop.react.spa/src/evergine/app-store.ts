import { createStore, useStore } from "evergine-react";

type AppState = {
  cameraClick: { rotation: number } | undefined;
};

const appStore = createStore<AppState>({ cameraClick: undefined });

const useAppStore = () => {
    const [state, setState] = useStore(appStore);

    const setCameraClick = (value: { rotation: number}) => {
        setState({cameraClick: value});
      };
  
    return {
      cameraClick: state.cameraClick,
      setCameraClick,
    };
  };

export { appStore, useAppStore };
