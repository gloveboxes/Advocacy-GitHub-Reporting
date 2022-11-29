import React from 'react';
import ComponentCreator from '@docusaurus/ComponentCreator';

export default [
  {
    path: '/docs/markdown-page/',
    component: ComponentCreator('/docs/markdown-page/', '391'),
    exact: true
  },
  {
    path: '/docs/',
    component: ComponentCreator('/docs/', 'a64'),
    routes: [
      {
        path: '/docs/',
        component: ComponentCreator('/docs/', '2bf'),
        exact: true,
        sidebar: "tutorialSidebar"
      }
    ]
  },
  {
    path: '*',
    component: ComponentCreator('*'),
  },
];
