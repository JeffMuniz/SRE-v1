
import styled, {css} from 'styled-components';
import {ErrorModal, Input} from '~/components';
import {mqTablet} from '~/styled';

export const Wrapper = styled.div`
  margin-bottom: 50px;
`;

export const CNPJInput = styled(Input).attrs({
  label: 'CNPJ:',
})`
  margin-bottom: 20px;

  ${mqTablet(css`
    flex-grow: 1;
  `)}
`;

export const TabletInlineWrapper = styled.div`
  ${mqTablet(css`
    display: flex;
    width: 100%;
  `)}
`;

export const NameInput = styled(CNPJInput).attrs<Input.Props>({
  label: 'Razão Social:',
})``;

const inputMarginBlock = css`
  input {
    padding-right: 70px;
  }
  img {
    margin-left: -60px;
  }
`;

export const TradingNameInput = styled(CNPJInput).attrs<Input.Props>({
  label: 'Nome da Empresa:',
  showEditIndicator: true,
})`
  ${mqTablet(css`
    ${inputMarginBlock}
  `)}
`;

export const ZipCodeInput = styled(CNPJInput).attrs<Input.Props>({
  label: 'CEP:',
  showEditIndicator: true,
})`
  ${mqTablet(css`
    max-width: 230px;
    input {
      padding-right: 70px;
    }
    img {
      margin-left: -60px;
    }
  `)}
`;

export const StreetInput = styled(CNPJInput).attrs<Input.Props>({
  label: 'Endereço:',
})``;

export const NumberInput = styled(NameInput).attrs<Input.Props>({
  label: 'Número:',
  showEditIndicator: true,
})`
  ${mqTablet(css`
    max-width: 150px;
  `)}
`;

export const AdditionalInfoInput = styled(NameInput).attrs<Input.Props>({
  label: 'Complemento:',
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

export const PhoneInput = styled(NameInput).attrs<Input.Props>({
  label: 'Telefone:',
  showEditIndicator: true,
})``;

export const NeighborhoodInput = styled(CNPJInput).attrs<Input.Props>({
  label: 'Bairro:',
})``;

export const CityInput = styled(NeighborhoodInput).attrs<Input.Props>({
  label: 'Cidade:',
})``;

export const StateInput = styled(CNPJInput).attrs<Input.Props>({
  label: 'Estado:',
})`
  ${mqTablet(css`
    max-width: 100px;
  `)}
`;

export const FErrorModal = styled(ErrorModal)``;
