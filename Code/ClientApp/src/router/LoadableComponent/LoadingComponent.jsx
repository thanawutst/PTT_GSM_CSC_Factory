import React, { Component } from 'react';
import NProgress from 'nprogress';
import './nprogress_custom.css';

NProgress.configure({ showSpinner: false });

export class ProgressBar {
    static start() {
        NProgress.start();
    }

    static done() {
        NProgress.done();
    }
}

export default class LoadingComponent extends Component {
    constructor(props) {
        super(props);
    
        this.state = {
          component: null,
        };
      }
    
      componentWillMount() {
        ProgressBar.start();
      }
    
      componentWillUnmount() {
        ProgressBar.done();
      }
    
      render() {
        return <div />;
      }
}