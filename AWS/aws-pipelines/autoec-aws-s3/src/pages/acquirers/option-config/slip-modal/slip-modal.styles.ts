
import styled, {css} from 'styled-components';
import {Modal} from '~/components';
import {
 BaseLabel, PageSubtitle, PrimaryButton, mqTablet,
} from '~/styled';

const {PUBLIC_URL} = process.env;

export const Wrapper = styled(Modal)``;

export const InfoLabel = styled(PageSubtitle)`
  align-self: center;
  margin-bottom: 20px;
`;

export const CieloSlip = styled.img.attrs({
  src: `${PUBLIC_URL}/images/macna-slip.png`,
})`
  align-self: center;
  margin-bottom: 20px;
`;

export const SafrapaySlip = styled(CieloSlip).attrs({
  src: `${PUBLIC_URL}/images/safrapay-slip.png`,
})``;

export const GetnetSlip = styled(CieloSlip).attrs({
  src: `${PUBLIC_URL}/images/getnet-slip.png`,
})``;

export const RedeSlip = styled(CieloSlip).attrs({
  src: `${PUBLIC_URL}/images/rede-slip.png`,
})``;

export const WarningTitle = styled(BaseLabel)`
  display: block;
  font-size: 14px;
  font-weight: bold;
  margin-bottom: 10px;

  ${({theme: {colors}}) => css`
    color: ${colors.palette.pink.main};
  `}
`;

export const WarningContent = styled(BaseLabel)`
  display: block;
  font-size: 14px;
  margin-bottom: 20px;
`;

export const CloseButton = styled(PrimaryButton)`
  min-height: 48px;

  ${mqTablet(css`
    align-self: center;
  `)}
`;
