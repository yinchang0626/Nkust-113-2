import { Main } from '@strapi/design-system';
import { useIntl } from 'react-intl';

import { getTranslation } from '../utils/getTranslation';

const HomePage = () => {
  const { formatMessage } = useIntl();

  return (
    <Main>
      <h1>hell0 test</h1>
      <h1>Welcome to {formatMessage({ id: getTranslation('plugin.name') })}</h1>
    </Main>
  );
};

export { HomePage };
