import React from 'react';
import ComponentCreator from '@docusaurus/ComponentCreator';

export default [
  {
    path: '/GitHub-Metrics-Endpoint/markdown-page/',
    component: ComponentCreator('/GitHub-Metrics-Endpoint/markdown-page/', 'e57'),
    exact: true
  },
  {
    path: '/GitHub-Metrics-Endpoint/',
    component: ComponentCreator('/GitHub-Metrics-Endpoint/', '87d'),
    routes: [
      {
        path: '/GitHub-Metrics-Endpoint/',
        component: ComponentCreator('/GitHub-Metrics-Endpoint/', '501'),
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
