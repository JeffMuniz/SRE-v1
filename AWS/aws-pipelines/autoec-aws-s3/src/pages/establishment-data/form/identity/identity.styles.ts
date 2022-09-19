
import {Fragment} from 'react';
import styled, {css} from 'styled-components';
import {Input} from '~/components';
import {mqTablet} from '~/styled';

export const Wrapper = Fragment;

export const TabletInlineWrapper = styled.div`
  ${mqTablet(css`
    display: flex;
    width: 100%;
  `)}
`;

export const inputSpacerBlock = css`
  input {
    padding-right: 70px;
  }
  img {
    margin-left: -60px;
  }
`;

export const CompanyNameInput = styled(Input)`
  margin-bottom: 20px;

  ${mqTablet(css`
    flex-grow: 1;
    ${inputSpacerBlock}
  `)}
`;

export const TradingNameInput = styled(Input)`
  margin-bottom: 20px;
  ${mqTablet(css`
    flex-grow: 1;
  `)}
`;

export const EstablishmentPhoneInput = styled(Input)`
  margin-bottom: 20px;
`;
