
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

  ${mqTablet(css`
    
  `)}
`;

export const CNPJInput = styled(Input).attrs({
  id: 'cnpj',
  placeholder: 'digite o seu cnpj',
  name: 'cnpj',
  label: 'Para começar, digite o seu CNPJ e confirme',
})`

  label {
    display: none;
  }

  ${mqTablet(css`
    margin-bottom: 20px;
    label {
      display: block;
    }
  `)}
`;

export const FSpacer = styled(Spacer)``;

export const ContinueButton = styled(LoadingButton).attrs({
  type: 'submit',
})`
  ${mqTablet(css`
    align-self: flex-end;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;
