import { useForm, FormProvider } from 'react-hook-form';
import { i18n } from 'src/i18n';
import React, { useEffect, useState } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';
import MaterialLink from '@material-ui/core/Link';
import { AuthenActionCreators } from "src/store/redux/AuthenStore";
import AuthenSelectors from "src/store/selectors/AuthenSelectors";
import Content from 'src/view/Auth/style/Content';
import Logo from 'src/view/Auth/style/Logo';
import OtherActions from 'src/view/Auth/style/OtherActions';
import Wrapper from 'src/view/Auth/style/Wrapper';
import backgroundImage from "src/images/login/softthai.png";
import InputFormItem from 'src/components/Commons/inputElements/FormItems/InputFormItem';
import {
  Checkbox,
  FormControlLabel,
  Box,
  Button,
} from '@material-ui/core';
import yupFormSchemas from 'src/components/Commons/yup/yupFormSchemas';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';

const i18nField = 'entities.auth.login.fields'
const schema = yup.object().shape({
  username: yupFormSchemas.string(i18n(`${i18nField}.username`), {
    required: true,
  }),
  password: yupFormSchemas.string(i18n(`${i18nField}.password`), {
    required: true,
  },
  ),

});

function LoginPage() {
  const dispatch = useDispatch();

  const loading = useSelector(AuthenSelectors.selectLoading);
  const externalErrorMessage = useSelector(
    AuthenSelectors.selectErrorMessage,
  );

  const initialValuesRedux = useSelector(
    AuthenSelectors.selectInputLogin,
  );



  const [initialValues] = useState({
    username: initialValuesRedux.sUsername || '',
    password: initialValuesRedux.sPassword || '',

  });
  const [isShowPass, SetShowPass] = useState(false);

  const form = useForm({
    resolver: yupResolver(schema),
    mode: 'all',
    defaultValues: initialValues,

  });

  const onSubmit = (values) => {
    const dataToLogin = {} as any;
    dataToLogin.sUsername = values.username;
    dataToLogin.sPassword = values.password;

    dispatch(AuthenActionCreators.doLoginWithUsernameAndPassword(dataToLogin));
  };

  useEffect(() => {
    dispatch(AuthenActionCreators.doClearErrorMessage());
  }, [dispatch]);


  const doShowPass = () => {
    SetShowPass((prev) => (!prev));
  }

  return (
    <Wrapper
      style={{
        backgroundImage: `url(${backgroundImage})`,
      }}
    >
      <Content>
        <Logo>
          {/* {logoUrl ? (
            <img
              src={logoUrl}
              width="240px"
              alt={i18n('app.title')}
            />
          ) : (
            <h1>{i18n('app.title')}</h1>
          )} */}
          <h1>{i18n('app.title')}</h1>
        </Logo>
        <FormProvider {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} >
            <InputFormItem
              name="username"
              label={i18n(`${i18nField}.username`)}
              autoComplete="off"
              autoFocus
              externalErrorMessage={externalErrorMessage}
            />

            <InputFormItem
              name="password"
              label={i18n(`${i18nField}.password`)}
              autoComplete="off"
              externalErrorMessage={externalErrorMessage}
              type={isShowPass ? "text" : "password"}
            />

            <Box
              display="flex"
              justifyContent="space-between"
              alignItems="center"
            >
              <FormControlLabel
                control={
                  <Checkbox
                    id={'showPassword'}
                    name={'showPassword'}
                    defaultChecked={isShowPass}
                    inputRef={form.register}
                    onChange={doShowPass}
                    color="primary"
                    size="small"
                  />
                }
                label={i18n(`${i18nField}.showPassword`)}
              />

            </Box>

            <Button
              style={{ marginTop: '8px' }}
              variant="contained"
              color="primary"
              type="submit"
              fullWidth
              disabled={loading}
            >
              {i18n(`${i18nField}.btnLogin`)}
            </Button>
          </form>
        </FormProvider>
      </Content>
    </Wrapper>
  );
}

export default LoginPage;
