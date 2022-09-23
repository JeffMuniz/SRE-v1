
import styled, {css} from 'styled-components';
import {Input} from '~/components';
import {mqTablet} from '~/styled';

export const Wrapper = styled.div`
  margin-bottom: 50px;

  ${mqTablet(css`
    display: flex;
    flex-wrap: wrap;
  `)}
`;

export const NameInput = styled(Input).attrs({
  label: 'Nome:',
})`
  margin-bottom: 20px;

  ${mqTablet(css`
    width: 50%;
  `)}
`;

export const CPFInput = styled(NameInput).attrs<Input.Props>({
  label: 'CPF:',
})``;

export const EmailInput = styled(NameInput).attrs<Input.Props>({
  label: 'Endereço:',
  showEditIndicator: true,
})`
  ${mqTablet(css`
    input {
      padding-right: 70px;
    }
    img {
      margin-left: -60px;
    }
  `)}
`;

export const CellphoneInput = styled(NameInput).attrs<Input.Props>({
  label: 'Telefone Celular:',
  showEditIndicator: true,
})`
  margin-bottom: 0;
`;
