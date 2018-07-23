/// <reference types="react" />
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
