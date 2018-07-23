/// <reference types="react" />
declare module "lib" {
    export const getText: () => string;
}
declare module "es6codelib" {
    export default class ES6Lib {
        _text: string;
        constructor();
        getData: () => string;
    }
}
declare module "reactcomponent" {
    import * as React from 'react';
    export interface ICounterState {
        count: number;
    }
    export default class Counter extends React.Component<null, ICounterState> {
        constructor(props: any);
        render(): JSX.Element;
        private incrementCount;
    }
}
declare module "fetchdata" {
    import * as React from 'react';
    import 'es6-promise';
    import 'isomorphic-fetch';
    export interface IFetchDataState {
        apiDataObjects: object[];
        loading: boolean;
    }
    export default class FetchData extends React.Component<null, IFetchDataState> {
        constructor(props: any);
        render(): JSX.Element;
        private refreshData;
    }
}
declare module "app" {
    import 'bootstrap/dist/css/bootstrap.min.css';
    import '../css/site.css';
}
