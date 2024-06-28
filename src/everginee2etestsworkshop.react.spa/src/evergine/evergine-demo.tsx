import React, { useEffect, useState } from "react";
import { useEvergineStore, EvergineCanvas } from "evergine-react";
import { EVERGINE_CANVAS_ID } from "evergine/config";
import { useAppStore } from "evergine/app-store";
import { useWindowSize } from "./useWindowSize";
import "evergine/evergine-demo.css";

function EvergineDemo(): JSX.Element {
  const [message, setMessage] = useState("");
  const [canvasActivated, setCanvasActivated] = useState(true);
  const size = useWindowSize();

  const { webAssemblyLoaded, evergineReady } = useEvergineStore();
  const { cameraClick } = useAppStore();

  useEffect(() => {
    if (cameraClick !== undefined) {
      setMessage(
        `Teapot current rotation: ${Math.round(cameraClick?.rotation)}º`
      );
    }
  }, [cameraClick]);

  return (
    <div className="Evergine-demo">
      {!webAssemblyLoaded && <div>Loading Evergine ...</div>}
      {webAssemblyLoaded && (
        <div>
          Try Evergine Events:
          <div>
            <button
              className="Button"
              onClick={() => initCanvas()}
              disabled={canvasActivated}
            >
              Initialize Canvas
            </button>
            <button
              className="Button"
              onClick={() => destroyCanvas()}
              disabled={!canvasActivated}
            >
              Destroy Canvas
            </button>
          </div>
          {evergineReady && canvasActivated && (
            <div>
              <button className="Button" onClick={() => fromReactToWasm()}>
                From React to Wasm
              </button>
              <button className="Button" onClick={() => fromWasmToReact()}>
                From Wasm to React
              </button>
            </div>
          )}
          <input
            title="result"
            value={message}
            readOnly
            className="Input"
          ></input>
        </div>
      )}

      <div className="Evergine-container">
        {canvasActivated && (
          <EvergineCanvas
            canvasId={EVERGINE_CANVAS_ID}
            width={300}
            height={size.height/4}
          />
        )}
      </div>

      <a
        className="App-link"
        href="https://docs.evergine.com/"
        target="_blank"
        rel="noopener noreferrer"
      >
        Learn Evergine
      </a>
    </div>
  );

  function initCanvas(): void {
    setMessage("Initiating Evergine Canvas");
    setCanvasActivated(true);
  }

  function destroyCanvas(): void {
    setMessage("Destroying Evergine Canvas");
    setCanvasActivated(false);
  }

  function fromReactToWasm(): void {
    const r = getRandomHex();
    const g = getRandomHex();
    const b = getRandomHex();
    const color = `#${r}${g}${b}`;
    window.App.webEventsProxy.changeColor(color);
    setMessage(`Changing the color of the teapot: ${color}`);
  }

  function getRandomHex() {
    return Math.floor(Math.random() * 256)
      .toString(16)
      .padStart(2, "0")
      .toUpperCase();
  }

  function fromWasmToReact(): void {
    setMessage("Click on the teapot to send the event 😉");
  }
}

export default EvergineDemo;
