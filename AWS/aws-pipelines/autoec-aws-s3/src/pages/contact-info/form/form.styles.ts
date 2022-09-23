
import styled, {css} from 'styled-components';
import {
ErrorModal, Input, LoadingButton,
} from '~/components';
import {Spacer, mqTablet} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const EmailInput = styled(Input)`
  margin-bottom: 20px;
`;

export const PhoneInput = styled(EmailInput)`
  margin-bottom: 0;
`;

export const FSpacer = styled(Spacer)``;

export const SubmitButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;
