import React from 'react';
import { Link } from 'react-router-dom';
import { i18n } from 'src/i18n';
import { Button } from '@material-ui/core';
import { styled } from '@material-ui/core/styles';
import { useLocation } from "react-router-dom";

const ErrorWrapper = styled('div')({
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    height: '80%',
    minHeight: '100vh',
    textAlign: 'center',

    '& .content': {
        '& h1': {
            color: '#434e59',
            fontSize: '72px',
            fontWeight: '600',
            lineHeight: '72px',
            marginBottom: '24px',
        },
        '& h3': {
            color: '#434e59',
            fontSize: '50px',
            fontWeight: '500',
            marginBottom: '24px',
        },

        '& code': {
            color: 'red',
            fontSize: '50px',
            fontWeight: '500',
            marginBottom: '24px',
        },

        '& .desc': {
            fontSize: '20px',
            lineHeight: '28px',
            marginBottom: '16px',
        },

        '& .actions': {
            '& button:not(:last-child)': {
                marginRight: '8px',
            },
        },
    },
});

const NotMatch = () => {
    let location = useLocation();
    return (
        <ErrorWrapper>
            <div className="exception">
                <div className="content">
                    <h1>404</h1>
                    <h3>
                        Not found: <code>{location.pathname}</code>
                    </h3>
                    <div className="desc">{i18n('errors.404')}</div>
                    <div className="actions">
                        <Button
                            component={Link}
                            to="/"
                            variant="contained"
                            color="primary"
                            type="button"
                        >
                            {i18n('errors.backToHome')}
                        </Button>
                    </div>
                </div>
            </div>
        </ErrorWrapper>
    );
};

export default NotMatch;
