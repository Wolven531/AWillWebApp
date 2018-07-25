/// <reference types="react" />
import * as React from 'react';
import 'es6-promise';
import 'isomorphic-fetch';
export interface IFetchDataState {
    loading: boolean;
    monsters: object[];
}
export default class FetchData extends React.Component<null, IFetchDataState> {
    constructor(props: any);
    render(): JSX.Element;
    private refreshData;
}
